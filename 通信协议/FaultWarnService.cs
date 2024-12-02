using AutoMapper;
using Confluent.Kafka;
using DataBase.DTO;
using DataBase.Tables;
using KP.Util;
using KP.Util.DTO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog.LayoutRenderers;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Xml.Linq;
using static Dm.net.buffer.ByteArrayBuffer;

namespace KAFKA_PARSE
{
    /// <summary>
    /// 预警报警定时服务
    /// </summary>
    public class FaultWarnService : BackgroundService
    {
        //同步预警报警分析系统平台处理结果
        private Timer _warnHandleResTimer;
        //定时删除任务
        private Timer _deleteTimer;
        private Timer _deleteCSTimer;
        //故障定时任务
        private Timer _faultTimer;
        //测试用
        private Timer _testTimer;
        //寿命推送
        private Timer _lifetTimer;
        //预警定时任务
        private Timer _warnTimer;
        //物模型上传卡夫卡任务
        private Timer _hvacmodleTimer;

        private readonly static Dictionary<string, string> _setting = Helper.LoadJsonData("GZML7");
        private readonly string _appId = _setting["AppId"];
        private readonly string _appKey = _setting["AppKey"];
        private readonly string _baseUrl = _setting["BaseUrl"];
        private readonly string _lineCode = _setting["LineCode"];
        private readonly string _GetHttpHandle = _setting["GetHttpHandleTime"];
        private readonly string _FaultDataPush = _setting["FaultDataPushTime"];
        private readonly string _AddOrUpdateSMData = _setting["AddOrUpdateSMDataTime"];
        private readonly string _AddWarnData = _setting["AddWarnDataTime"];
        private readonly string _XieruKafkaAsync = _setting["XieruKafkaAsync"];


        private readonly ILogger<FaultWarnService> _logger;
        //private readonly SqlSugarClient _db;
        private readonly MyDbContext _db;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public FaultWarnService(IOptions<AppSettings> appSettings, ILogger<FaultWarnService> logger, MyDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _db = dbContext;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {

            _deleteTimer = new Timer(async _ =>
            {

                _logger.LogInformation("定时删除任务");

                await Task.Run(async () =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        // 计算今天午夜的时间（假设现在是UTC时间，你可能需要调整为本地时间）  
                        DateTime now = DateTime.Now;
                        DateTime midnight = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, DateTimeKind.Local);

                        // 如果已经过了今天的午夜，则计算明天的午夜  
                        if (now > midnight)
                        {
                            midnight = midnight.AddDays(1);
                        }

                        // 计算距离下一次午夜还有多长时间  
                        TimeSpan timeToWait = midnight - now;

                        // 等待直到午夜  
                        try
                        {
                            await Task.Delay(timeToWait, cancellationToken);

                            // 执行你的任务  
                            await Delete();
                        }
                        catch (TaskCanceledException)
                        {
                            // 计时器被取消，退出循环  
                            break;
                        }
                    }
                });

            }, null, TimeSpan.Zero, TimeSpan.FromDays(1));

            _deleteCSTimer = new Timer(async _ =>
            {
                await Console.Out.WriteLineAsync("定时删除实时数据");
                await DeleteCS();
            }, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            //_warnHandleResTimer = new Timer(async _ =>
            //{
            //    _logger.LogInformation("开始同步分析平台处理结果");
            //    await GetHttpHandle();
            //}, null, TimeSpan.Zero, TimeSpan.FromMinutes(Convert.ToDouble(_GetHttpHandle)));


            //_testTimer = new Timer(async _ =>
            //{
            //    _logger.LogInformation("开始添加测试数据");
            //    //await GetHttpHandle();

            //}, null, TimeSpan.Zero, TimeSpan.FromDays(1));


            _faultTimer = new Timer(async _ =>
            {
                _logger.LogInformation("开始同步故障");
                await FaultDataPush();

            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(Convert.ToDouble(_FaultDataPush)));

            _lifetTimer = new Timer(async _ =>
            {
                _logger.LogInformation("开始更新寿命");

                //await AddOrUpdateSMDataV1();

            }, null, TimeSpan.Zero, TimeSpan.FromHours(Convert.ToDouble(_AddOrUpdateSMData)));

            _warnTimer = new Timer(async _ =>
            {
                _logger.LogInformation("开始预警");

                try
                {
                    await GetSfwdFault();
                    await GetZlmbwdFault();
                    await GetZlxtylFault();
                    await GetSmFault();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"预警失败，{ex.ToString()}");
                }


            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(Convert.ToDouble(_AddWarnData)));

            //_hvacmodleTimer = new Timer(async _ =>
            //{
            //    _logger.LogInformation("物模型写入卡夫卡");
            //    await XieruKafkaAsync();

            //}, null, TimeSpan.Zero, TimeSpan.FromMinutes(Convert.ToDouble(_XieruKafkaAsync)));

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _faultTimer?.Change(Timeout.Infinite, 0);
            _lifetTimer?.Change(Timeout.Infinite, 0);
            _warnHandleResTimer?.Change(Timeout.Infinite, 0);
            _warnTimer?.Change(Timeout.Infinite, 0);
            _deleteTimer?.Change(Timeout.Infinite, 0);
            _hvacmodleTimer?.Change(Timeout.Infinite, 0);
            _deleteCSTimer?.Change(Timeout.Infinite, 0);
            await Task.CompletedTask;
        }
        public async Task DeleteCS()
        {
            try
            {
                var time = DateTime.Now;
                var tableName = _db.SplitHelper<TB_PARSING_DATAS_CS>().GetTableName(time);//根据时间获取表名
                var row = await _db.Deleteable<TB_PARSING_DATAS_CS>()
                    .Where(x => x.create_time <= time.AddHours(-1))
                    .SplitTable(tabs => tabs.InTableNames(tableName))
                    .ExecuteCommandAsync();
                await Console.Out.WriteLineAsync($"成功删除了{row}条数据");
            }
            catch (Exception ex)
            {
                _logger.LogError($"TB_PARSING_DATAS_CS删除失败,{ex}");
            }
            
        }

        /// <summary>
        /// 删除一个月前的数据
        /// </summary>
        /// <returns></returns>
        public async Task Delete()
        {
            var date30 = DateTime.Today.AddDays(-180).ToString("yyyyMMdd");
            var date1 = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
           
            try
            {
                var sql = $@"if object_id('TB_PARSING_DATAS_{date30}','U') is not null 
                           drop table  TB_PARSING_DATAS_{date30}";

                var sql1 = $@"if object_id('TB_PARSING_DATAS_CS_{date1}','U') is not null 
                           drop table  TB_PARSING_DATAS_CS_{date1}";

                //var sql2 = $@"if object_id('TB_PARSING_DATAS_YJ_1_{date1}','U') is not null 
                //           drop table  TB_PARSING_DATAS_YJ_1_{date1}";

                var sql3 = $@"if object_id('TB_PARSING_DATAS_YJ_2_{date1}','U') is not null 
                           drop table  TB_PARSING_DATAS_YJ_2_{date1}";

                var ysbwSql = $@"if object_id('TB_YSBW_{date30}','U') is not null 
                           drop table TB_YSBW_{date30}";

                //删除过期数据
                await _db.Ado.ExecuteCommandAsync(sql);
                await _db.Ado.ExecuteCommandAsync(sql1);
                //await _db.Ado.ExecuteCommandAsync(sql2);
                await _db.Ado.ExecuteCommandAsync(sql3);
                await _db.Ado.ExecuteCommandAsync(ysbwSql);

                await Console.Out.WriteLineAsync("删除成功");
            }
            catch (Exception ex)
            {
                _logger.LogError($"删除失败,{ex}");
            }

        }

        /// <summary>
        /// 更新寿命数据并推送
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task AddOrUpdateSMDataV1()
        {
            var result = new List<PartsLife>();
            var updateLife = new List<DEVPARTS>();

            Random random = new();

            try
            {
                var nowLifeSql = $@"SELECT *                                         
                                    FROM
                                         dbo.TB_PARSING_DATAS_YJ_2" + $"_{DateTime.Now:yyyyMMdd}" +
                                    $@" WHERE
									     create_time >= DATEADD(HOUR,-1,GETDATE())"; 

                var lifeData = await _db.SqlQueryable<TB_PARSING_DATAS_YJ_2>(nowLifeSql).ToListAsync();
                if (lifeData.Count == 0) return;
             
                var devList = await _db.Queryable<DEVICE>().ToListAsync();
                var devPartsList = await _db.Queryable<DEVPARTS>().Where(x =>x.state == 0).ToListAsync();
                // 创建一个字典来快速查找部件  
                var devPartsDict = devPartsList.ToDictionary(x => (x.sbid, x.lbjid));

                foreach (var d in devList)
                {
                    int randomNumber = random.Next(1000, 2001);

                    var devData = lifeData.Where(x => x.device_code == d.device_id).ToList();

                    // 计算每个条件的计数  
                    var counts = new Dictionary<string, double>
                    {
                        { "jz1ysj1", devData.Count(x => x.jz1ysj1yx == "1") / 60.0 },
                        { "jz1ysj2", devData.Count(x => x.jz1ysj2yx == "1") / 60.0 },
                        { "jz2ysj1", devData.Count(x => x.jz2ysj1yx == "1") / 60.0 },
                        { "jz2ysj2", devData.Count(x => x.jz2ysj2yx == "1") / 60.0 },
                        { "jz1lnfj1", devData.Count(x => x.jz1lnfj1yx == "1") / 60.0 },
                        { "jz1lnfj2", devData.Count(x => x.jz1lnfj2yx == "1") / 60.0 },
                        { "jz2lnfj1", devData.Count(x => x.jz2lnfj1yx == "1") / 60.0 },
                        { "jz2lnfj2", devData.Count(x => x.jz2lnfj2yx == "1") / 60.0 },
                        { "jz1zffj1", devData.Count(x => x.jz1zffj1yx == "1") / 60.0 },
                        { "jz1zffj2", devData.Count(x => x.jz1zffj2yx == "1") / 60.0 },
                        { "jz2zffj1", devData.Count(x => x.jz2zffj1yx == "1") / 60.0 },
                        { "jz2zffj2", devData.Count(x => x.jz2zffj2yx == "1") / 60.0 },
                        { "jz1lnfj1zc", devData.Count(x => x.jz1lnfj1yx == "1") / 60.0 },
                        { "jz1lnfj2zc", devData.Count(x => x.jz1lnfj2yx == "1") / 60.0 },
                        { "jz2lnfj1zc", devData.Count(x => x.jz2lnfj1yx == "1") / 60.0 },
                        { "jz2lnfj2zc", devData.Count(x => x.jz2lnfj2yx == "1") / 60.0 },
                        { "jz1zffj1zc", devData.Count(x => x.jz1zffj1yx == "1") / 60.0 },
                        { "jz1zffj2zc", devData.Count(x => x.jz1zffj2yx == "1") / 60.0 },
                        { "jz2zffj1zc", devData.Count(x => x.jz2zffj1yx == "1") / 60.0 },
                        { "jz2zffj2zc", devData.Count(x => x.jz2zffj2yx == "1") / 60.0 },
                        { "jz1zwxd", devData.Count(x => x.jz1zwxdyx == "1") / 60.0 },
                        { "jz2zwxd", devData.Count(x => x.jz2zwxdyx == "1") / 60.0 }
                    };

                    // 更新每个相关部件的寿命  
                    foreach (var (condition, count) in counts)
                    {
                        var (sbid, lbjid) = condition switch
                        {
                            "jz1ysj1" => (d.id, 96),
                            "jz1ysj2" => (d.id, 97),
                            "jz2ysj1" => (d.id, 42),
                            "jz2ysj2" => (d.id, 43),
                            "jz1lnfj1" => (d.id, 94),
                            "jz1lnfj2" => (d.id, 95),
                            "jz2lnfj1" => (d.id, 40),
                            "jz2lnfj2" => (d.id, 41),
                            "jz1zffj1" => (d.id, 92),
                            "jz1zffj2" => (d.id, 93),
                            "jz2zffj1" => (d.id, 38),
                            "jz2zffj2" => (d.id, 39),
                            "jz1lnfj1zc" => (d.id, 137),
                            "jz1lnfj2zc" => (d.id, 138),
                            "jz2lnfj1zc" => (d.id, 139),
                            "jz2lnfj2zc" => (d.id, 140),
                            "jz1zffj1zc" => (d.id, 133),
                            "jz1zffj2zc" => (d.id, 134),
                            "jz2zffj1zc" => (d.id, 135),
                            "jz2zffj2zc" => (d.id, 136),
                            "jz1zwxd" => (d.id, 141),
                            "jz2zwxd" => (d.id, 142),
                            _ => throw new InvalidOperationException("Unknown condition")
                        };

                        if (devPartsDict.TryGetValue((sbid, lbjid), out var part))
                        {
                            if (part.servicelife == 0)
                            {
                                part.servicelife = (int?)(randomNumber + count);
                            }
                            else
                            {
                                part.servicelife += (int)count;
                            }
                            updateLife.Add(part);

                        }
                    }
                }

                var updateNum = await _db.Updateable(updateLife)
                         .WhereColumns(it => it.id)
                         .UpdateColumns(it => new { it.servicelife })
                         .ExecuteCommandAsync();
                await Console.Out.WriteLineAsync($"寿命数据更新成功,更新了{updateNum}条数据");

                //var pushLifeData = result.Where(x => x.FaultCode != null).ToList();

                ////寿命推送
                //pushLifeData = pushLifeData.Where(x => !string.IsNullOrEmpty(x.FarecastCode)).ToList();
                //await LifeDataPush(pushLifeData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"更新寿命数据时出错：{ex.ToString()}");
            }
        }

        /// <summary>
        /// 寿命推送
        /// </summary>
        /// <returns></returns>
        private async Task LifeDataPush(List<PartsLife> partsLife)
        {
            string urlType = "life-prediction";

            // 构建app_token
            string appToken = $"app_id={_appId}&app_key={_appKey}&date=" + DateTime.Now.ToString("yyyy-MM-dd");
            string tokenMd5 = Helper.GetMD5String(appToken).ToUpper();
            string url = $"{_baseUrl}{urlType}";

            List<LifeResDTO> newlife = new List<LifeResDTO>();

            var data = new LifeHttpResDTO();

            try
            {
                foreach (var part in partsLife)
                {
                    newlife.Add(new LifeResDTO
                    {
                        line_code = _lineCode,
                        train_code = part.CH,
                        coach_no = part.CX,
                        life_code = part.FarecastCode,
                        prediction_time = DateTime.Now.ToString(),
                        running_day = (part.RunLife / 24).ToString(),
                        residual_day = ((part.RatedLife - part.RunLife) / 24).ToString()
                    });
                }

                data.app_id = _appId;
                data.app_token = tokenMd5;
                data.lifes = newlife;
                var trainSta = await HttpClientExample.SendPostRequestAsync<HttpReq>(url, data);
                if (trainSta != null)
                {
                    if (trainSta.result_code == "200")
                    {
                        await Console.Out.WriteLineAsync("寿命数据推送成功");
                    }
                    else
                    {
                        await Console.Out.WriteLineAsync("寿命数据推送失败");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"寿命数据推送错误，{ex.ToString()}");
            }

        }

        /// <summary>
        /// 添加/推送故障信息
        /// </summary>
        /// <returns></returns>
        private async Task FaultDataPush()
        {
            var addFaults = new List<FAULTWARN>();
            var updateFaults = new List<FAULTWARN>();

            try
            {
                var config = _db.Queryable<SYS_CONFIG>().ToList();
                //获取故障时间
                var sj = config.Where(x => x.concode == "WarnSj").First()?.conval;           

                string sql = @"SELECT
	                                gz.jz2zlxt2dykgylgz, 
	                                gz.jz2zlxt1dykgylgz, 
	                                gz.jz1zlxt2dykgylgz, 
	                                gz.jz1zlxt1dykgylgz, 
	                                gz.jz2zlxt2dycgqgz, 
	                                gz.jz2zlxt1dycgqgz, 
	                                gz.jz1zlxt2dycgqgz, 
	                                gz.jz1zlxt1dycgqgz, 
	                                gz.jz1kqjhqgz, 
	                                gz.jz1yccgqgz, 
	                                gz.jz2kqjhqgz, 
	                                gz.jz2ygcgqgz, 
	                                gz.jz2zlxt1gykgylgz, 
	                                gz.jz2zlxt2gykgylgz, 
	                                gz.jz1zlxt2gykgylgz, 
	                                gz.jz1zlxt1gykgylgz, 
	                                gz.jz2zlxt2gycgqgz, 
	                                gz.jz2zlxt1gycgqgz, 
	                                gz.jz1zlxt2gycgqgz, 
	                                gz.jz1zlxt1gycgqgz, 
	                                gz.jz2hsfwdcgq1gz, 
	                                gz.jz2shfwdcgqgz, 
	                                gz.jz1hsfwdcgq1gz, 
	                                gz.jz1shfwdcgqgz, 
	                                gz.jz1zwxd2gz, 
	                                gz.jz1zwxd1gz, 
	                                gz.jz2zwxd2gz, 
	                                gz.jz2zwxd1gz, 
	                                gz.jz1xqwdcgq2gz, 
	                                gz.jz2xqwdcgq2gz, 
	                                gz.jz2xqwdcgq1gz, 
	                                gz.jz1pqwdcgq2gz, 
	                                gz.jz1pqwdcgq1gz, 
	                                gz.jz2pqwdcgq2gz, 
	                                gz.jz1xqwdcgq1gz, 
	                                gz.jz2pqwdcgq1gz, 
	                                gz.jz1lnwdcgq2gz, 
	                                gz.jz1lnwdcgq1gz, 
	                                gz.jz2lnwdcgq2gz, 
	                                gz.jz2lnwdcgq1gz, 
	                                gz.jz1bpq2yxgz, 
	                                gz.jz1bpq1yxgz, 
	                                gz.jz2bpq2yxgz, 
	                                gz.jz2bpq1yxgz, 
	                                gz.jz1jjtfjcqgz, 
	                                gz.jz2jjtfjcqgz, 
	                                gz.jz1zctfjcqgz, 
	                                gz.jz2zctfjcqgz, 
	                                gz.jz1lnfj2jcqgz, 
	                                gz.jz1lnfj1jcqgz, 
	                                gz.jz2lnfj2jcqgz, 
	                                gz.jz2lnfj1jcqgz, 
	                                gz.jz1ysj2cqgz, 
	                                gz.jz1ysj1cqgz, 
	                                gz.jz2ysj2cqgz, 
	                                gz.jz2ysj1cqgz, 
	                                gz.jz1dzpzf2txgz, 
	                                gz.jz1dzpzf1txgz, 
	                                gz.jz2dzpzf2txgz, 
	                                gz.jz2dzpzf1txgz, 
	                                gz.jz1cjmktxgz, 
	                                gz.jz2cjmktxgz, 
	                                gz.jz1kqzlmktxgz, 
	                                gz.jz2kqzlmktxgz, 
	                                gz.jz1bpq2txgz, 
	                                gz.jz1bpq1txgz, 
	                                gz.jz2bpq2txgz, 
	                                gz.jz2bpq1txgz, 
	                                gz.jz1sxjcgz, 
	                                gz.jz2sxjcgz, 
	                                gz.dlcjmktxgz, 
	                                gz.jz2ysj1pqwdbhgz, 
	                                gz.jz1ysj1pqwdbhgz, 
	                                gz.jz2hsfwdcgq2gz, 
	                                gz.jz2xfwdcgqgz, 
	                                gz.jz1hsfwdcgq2gz, 
	                                gz.jz1xfwdcgqgz, 
	                                gz.jz2ysj2pqwdbhgz, 
	                                gz.jz1ysj2pqwdbhgz, 
	                                gz.jz2xfddff2gz, 
	                                gz.jz1hfddff1gz, 
	                                gz.jz1hfddff2gz, 
	                                gz.jz1xfddff2gz, 
	                                gz.jz2hfddff1gz, 
	                                gz.jz2xfddff1gz, 
	                                gz.jz1xfddff1gz, 
	                                gz.jz2hfddff2gz,
                                    gz.ktjjnbqgz,
                                    gz.jz2zlxt2xlyj, 
	                                gz.jz2zlxt1xlyj, 
	                                gz.jz1zlxt2xlyj, 
	                                gz.jz1zlxt1xlyj, 
	                                gz.jz2lwzdyj, 
	                                gz.jz1lwzdyj, 
	                               
	                                gz.jz2ysj2smyj, 
	                                gz.jz2ysj1smyj, 
	                                gz.jz1ysj2smyj, 
	                                gz.jz1ysj1smyj, 
	                              
	                                gz.jz1lnfj1smyj, 
	                                gz.jz1lnfj2smyj, 
	                                gz.jz2lnfj1smyj, 
	                                gz.jz2lnfj2smyj, 
	                                gz.jz1zffj1smyj, 
	                                gz.jz1zffj2smyj, 
	                                gz.jz2zffj1smyj, 
	                                gz.jz2zffj2smyj, 
	                                gz.cnkqzlyj,
	                                gz.rq,
                                    gz.rqDateTime,
	                                gz.create_time, 
	                                gz.xlid, 
	                                gz.lch, 
	                                gz.cxh, 
	                                gz.device_code
								FROM
									dbo.TB_PARSING_DATAS" + $"_{DateTime.Now:yyyyMMdd} " + "AS gz " +
                                $@"WHERE
									gz.create_time >= DATEADD(MINUTE,-{sj},GETDATE())";

                //获取故障编码
                var equipments = await _db.Queryable<OVERHAULIDEA>().Where(x => !string.IsNullOrEmpty(x.gzval)).ToListAsync();
                var faultData = _db.Queryable<FAULTWARN>().ToList();
                var devicData = _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToList();

                var lchs = _db.Queryable<LCH>().ToList();
                var cxhs = _db.Queryable<CXH>().ToList();
                //获取故障数据
                var faults = await _db.SqlQueryable<FaultDTO>(sql).OrderBy(x => x.rq).ToListAsync();
                if (faults.Count == 0)
                    return;
                
                foreach (var item in faults)
                {
                    var dic = GetPropertiesAndValues(item);
                    var faultdic = dic.Where(x => x.Value.ToString() == "1");
                    var lcid = lchs.FirstOrDefault(x => x.lch == item.lch);
                    var cxid = cxhs.FirstOrDefault(x => x.lcid == lcid.id && x.cxh == item.cxh);
                    var sbData = devicData.FirstOrDefault(x =>x.device_code == item.device_code);

                    foreach (var item1 in faultdic)
                    {
                        var faultCode = equipments.Where(x => x.gzval == item1.Key).FirstOrDefault();
                        if (faultCode == null) continue;

                        var isAny1 = faultData.Any(x => x.sbbm == item.device_code && x.xdid == faultCode.id && x.state == "0");
                        if (isAny1) continue;

                        var isAny2 = addFaults.Any(x => x.sbbm == item.device_code && x.xdid == faultCode.id && x.state == "0");
                        if (isAny2) continue;
                        
                        var faultOrWarn = new FAULTWARN
                        {
                            line_id = Convert.ToInt32(lcid.xl_id),
                            lch = item.lch,
                            lcid = lcid.id,
                            cxid = cxid.id,
                            cxh = item.cxh,
                            sbid = (int?)sbData.id,
                            sbbm = item.device_code,
                            xdid = faultCode.id,
                            type = faultCode.type,
                            xdmc = faultCode.jxname,
                            state = "0",
                            gzjb = faultCode.gzdj,
                            collect_time = item.rqDateTime,
                            createtime = DateTime.Now,
                            occ_name = faultCode.reason,
                            sfxztb = Guid.NewGuid().ToString("N")

                        };
                        addFaults.Add(faultOrWarn);
                    }
                }

                var faultOn = faultData.Where(x => x.state == "0" && x.type == "3").ToList();
                foreach (var On in faultOn)
                {
                    var Aname = equipments.Where(x => x.id == On.xdid).FirstOrDefault();
                    if (Aname == null) continue;

                    var newData = faults.Where(x => x.device_code == On.sbbm).ToList();
                    if (newData.Count == 0) continue;
                 
                    var newDataFirst = newData.OrderByDescending(x => x.rqDateTime).FirstOrDefault();
                    var dic = GetPropertiesAndValues(newDataFirst);
                    dic.TryGetValue(Aname.gzval, out var value);
                    if (value.ToString() == "0")
                    {
                        On.state = "2";
                        On.updatetime = DateTime.Now;
                        updateFaults.Add(On);
                    }
                }

                var num = _db.Insertable(addFaults).ExecuteCommand();
                var num1 = _db.Updateable(updateFaults).ExecuteCommand();

                var faultSet = addFaults.Where(x => x.occ_name == "yj").ToList();
                var data = await FaultSetHttpPost(faultSet);

                _logger.LogInformation($"故障同步完成，新增了{num}条故障,关闭了{num1}条故障");

            }
            catch (Exception ex)
            {
                _logger.LogError($"故障信息添加失败，{ex}");
            };
        }

        static Dictionary<string, object> GetPropertiesAndValues<T>(T obj)
        {
            var dict = new Dictionary<string, object>();

            // 获取对象的Type信息  
            Type type = obj.GetType();

            // 获取所有公共属性的PropertyInfo数组  
            PropertyInfo[] properties = type.GetProperties();

            // 遍历属性  
            foreach (PropertyInfo prop in properties)
            {
                // 获取属性值并添加到字典中  
                // 注意：这里假设所有属性都有getter，否则会抛出异常  
                dict.Add(prop.Name, prop.GetValue(obj, null));
            }

            return dict;
        }

        /// <summary>
        /// 获取平台故障预警处理信息并更新
        /// </summary>
        /// <returns></returns>
        public async Task GetHttpHandle()
        {
            string urlType = "warn-status";

            // 构建app_token
            string appToken = $"app_id={_appId}&app_key={_appKey}&date=" + DateTime.Now.ToString("yyyy-MM-dd");
            string tokenMd5 = Helper.GetMD5String(appToken).ToUpper();
            var updateFaults = new List<FaultOrWarn>();
            try
            {
                var q = _db.Queryable<FaultOrWarn>().Where(x => x.State == "1" && x.Type == "2" && x.SendRepId != 0);
                var idList = q.OrderByDescending(x => x.createtime).Select(x => x.SendRepId).ToList();
                              
                var groups = idList.Chunk(300);

                foreach (var group in groups)
                {                   
                    var ids = string.Join(",", group);
                    string url = $"{_baseUrl}{urlType}?app_id={_appId}&app_token={tokenMd5}&warn_id={ids}";
                    var faultReq = await HttpClientExample.SendGetRequestAsync<HttpReq<List<WarnStateReq>>>(url);
                    if (faultReq != null)
                    {
                        var faultstate = faultReq.result_data.Where(x => x.status == 0).Select(x => x.id) ;
                        var data = q.Where(x => faultstate.Contains(x.SendRepId)).ToList();            
                        var updatenum = await _db.Updateable(data).UpdateColumns(x => new FaultOrWarn { State = "0",updatetime = DateTime.Now}).ExecuteCommandAsync();
                        _logger.LogInformation($"预警同步完成，更新了{updatenum}条预警");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"报警更新失败，{ex.ToString()}");
            }
        }
        //string url = "http://IP:PORT/gate/METRO-PHM/api/faultRecordsSubsystem/saveRecord";

        /// <summary>
        /// 预警推送
        /// </summary>
        /// <param name="newFault"></param>
        /// <returns></returns>
        public async Task<HttpWarnReq> FaultSetHttpPost(List<FAULTWARN> newFault,bool isOff = false)
        {
            try
            {
                var new_faults = new List<WarnPushDTO>();
                var faultCode = _db.Queryable<OVERHAULIDEA>().ToList();

                foreach (var item in newFault)
                {
                    var faultcode = faultCode.First(x => x.id == item.xdid);
                    var fault = new WarnPushDTO
                    {
                        id = item.sfxztb,
                        message_type = "1",
                        train_type = "",
                        train_no = item.lch,
                        coach = item.cxh,
                        location = item.gzbjmc,
                        code = faultcode.faultcode,
                        station1 = "",
                        station2 = "",
                        starttime = ToUnixTimestampMilliseconds(item.createtime),
                        subsystem = "5",
                        endtime = ToUnixTimestampMilliseconds(item.updatetime)
                    };
                    new_faults.Add(fault);
                }

                var headers = new Dictionary<string, string>();
                headers.Add("x-token", "T@-gp0a*2+aLc!G@+vk$G6A5+qtQW!FVT&O^FW6fdOijs-");

                var faultReq = await HttpClientExample.SendPostRequestAsync<HttpWarnReq>(_baseUrl, new_faults, headers);
                return faultReq;
            }
            catch (Exception ex)
            {
                _logger.LogError($"预警推送失败，{ex.ToString()}");
                return null;
                
            }          
        }

        // 将DateTime转换为Unix时间戳（毫秒）  
        public static string? ToUnixTimestampMilliseconds(DateTime? dateTime)
        {
            if (dateTime != null)
            {
                // 确保传入的是UTC时间，如果不是则先转换为UTC  
                DateTime? utcDateTime = dateTime?.Kind == DateTimeKind.Utc ? dateTime : dateTime?.ToUniversalTime();

                // 计算与Unix纪元（1970年1月1日）之间的差值，并转换为毫秒  
                var rerult = (utcDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))?.TotalSeconds;

                return rerult.ToString();
            }

            return null;
        }

        #region 预警模型
        /// <summary>
        /// 送风温度异常预警/每分钟预警
        /// </summary>
        /// <returns></returns>
        private async Task GetSfwdFault()
        {
            try
            {
                var addFaults = new List<FAULTWARN>();
                //var config = _db.Queryable<SYS_CONFIG>().ToList();
                var nowData = await _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToListAsync();
                var equipments = await _db.Queryable<OVERHAULIDEA>().ToListAsync();

                var lch = _db.Queryable<LCH>().ToList();

                var faultData = _db.Queryable<FAULTWARN>().Where(x => x.state == "0").ToList();

                string sql = @"SELECT * FROM
							    TB_PARSING_DATAS_YJ_2" + $"_{DateTime.Now:yyyyMMdd} " +
                         @"WHERE
								create_time >= DATEADD(MINUTE,-3,GETDATE())";

                var newDatas = await _db.SqlQueryable<TB_PARSING_DATAS_YJ_2>(sql).ToListAsync();
                if (newDatas.Count == 0) return;

                var jz1sfWarn1 = equipments.First(x => x.faultcode == "HVAC06L0110");
                var jz1sfWarn2 = equipments.First(x => x.faultcode == "HVAC06L0120");
                var jz2sfWarn1 = equipments.First(x => x.faultcode == "HVAC06L0210");
                var jz2sfWarn2 = equipments.First(x => x.faultcode == "HVAC06L0220");

                var jz1xf = equipments.First(x => x.faultcode == "HVAC04L0100");
                var jz2xf = equipments.First(x => x.faultcode == "HVAC04L0200");

                var jz1hf = equipments.First(x => x.faultcode == "HVAC05L0100");
                var jz2hf = equipments.First(x => x.faultcode == "HVAC05L0200");

                var time = DateTime.Now.AddMinutes(-1);
                var data1mut = newDatas.Where(x => x.create_time >= time).ToList();

                foreach (var item in lch)
                {
                    var lcData = newDatas.Where(x => x.lch == item.lch).ToList();
                    if (lcData.Count == 0) continue;

                    var hfavg = lcData.Average(x => (Convert.ToDecimal(x.jz1shfwd) + Convert.ToDecimal(x.jz2shfwd))/2);
                    var xfavg = lcData.Average(x => (Convert.ToDecimal(x.jz1xfwd) + Convert.ToDecimal(x.jz2xfwd))/2);
                    foreach (var n in nowData)
                    {
                        var jz1xffault = !faultData.Any(x => x.xdid == jz1xf.id && x.sbid == item.id);
                        var jz2xffault = !faultData.Any(x => x.xdid == jz2xf.id && x.sbid == item.id);

                        var jz1hffault = !faultData.Any(x => x.xdid == jz1hf.id && x.sbid == item.id);
                        var jz2hffault = !faultData.Any(x => x.xdid == jz2hf.id && x.sbid == item.id);

                        var data = lcData.Where(x => x.device_code == n.device_code).ToList();
                        if (data.Count == 0) continue;

                        var jz1xfT = !data.Any(x => Math.Abs(Convert.ToDecimal(x.jz1xfwd) - xfavg) <= 7);
                        var jz2xfT = !data.Any(x => Math.Abs(Convert.ToDecimal(x.jz2xfwd) - xfavg) <= 7);

                        var gzmsT = !data.Any(x => x.jz1gzms != "6" && x.jz2gzms != "6");

                        var jz1hfT = !data.Any(x =>
                        Math.Abs(Convert.ToDecimal(x.jz1shfwd) - hfavg) <= 7 &&
                        Math.Abs(Convert.ToDecimal(x.jz1shfwd) - Convert.ToDecimal(x.jz2shfwd)) <= 7 
                        );

                        var jz2hfT = !data.Any(x =>
                        Math.Abs(Convert.ToDecimal(x.jz2shfwd) - hfavg) <= 7 &&
                        Math.Abs(Convert.ToDecimal(x.jz1shfwd) - Convert.ToDecimal(x.jz2shfwd)) <= 7
                        );

                        if (jz1xffault && jz1xfT)
                        {
                            var addFault = GetFAULTWARN(n, jz1xf);
                            addFaults.Add(addFault);
                        }

                        if (jz2xffault && jz2xfT)
                        {
                            var addFault = GetFAULTWARN(n, jz2xf);
                            addFaults.Add(addFault);
                        }

                        if (jz1hffault && jz1hfT && gzmsT)
                        {
                            var addFault = GetFAULTWARN(n, jz1hf);
                            addFaults.Add(addFault);
                        }

                        if (jz2hffault && jz2hfT && gzmsT)
                        {
                            var addFault = GetFAULTWARN(n, jz2hf);
                            addFaults.Add(addFault);
                        }
                    }
                }

                foreach (var item in nowData)
                {
                    var jz1sf1fault = !faultData.Any(x => x.xdid == jz1sfWarn1.id && x.sbid == item.id);
                    var jz1sf2fault = !faultData.Any(x => x.xdid == jz1sfWarn2.id && x.sbid == item.id);
                    var jz2sf1fault = !faultData.Any(x => x.xdid == jz2sfWarn1.id && x.sbid == item.id);
                    var jz2sf2fault = !faultData.Any(x => x.xdid == jz2sfWarn2.id && x.sbid == item.id);

                    var data = data1mut.Where(x => x.device_code == item.device_code).ToList();
                    if (data.Count == 0) continue;

                    var jz1sf1T = !data.Any(x => Convert.ToDouble(x.jz1hsfwd1) >= -10 && Convert.ToDouble(x.jz1hsfwd1) <= 40);
                    var jz1sf2T = !data.Any(x => Convert.ToDouble(x.jz1hsfwd2) >= -10 && Convert.ToDouble(x.jz1hsfwd2) <= 40);
                    var jz2sf1T = !data.Any(x => Convert.ToDouble(x.jz2hsfwd1) >= -10 && Convert.ToDouble(x.jz2hsfwd1) <= 40);
                    var jz2sf2T = !data.Any(x => Convert.ToDouble(x.jz2hsfwd2) >= -10 && Convert.ToDouble(x.jz2hsfwd2) <= 40);

                    if (jz1sf1fault && jz1sf1T)
                    {
                        var addFault = GetFAULTWARN(item, jz1sfWarn1);
                        addFaults.Add(addFault);
                    }
                    if (jz1sf2fault && jz1sf2T)
                    {
                        var addFault = GetFAULTWARN(item, jz1sfWarn2);
                        addFaults.Add(addFault);
                    }
                    if (jz2sf1fault && jz2sf1T)
                    {
                        var addFault = GetFAULTWARN(item, jz2sfWarn1);
                        addFaults.Add(addFault);
                    }
                    if (jz2sf2fault && jz2sf2T)
                    {
                        var addFault = GetFAULTWARN(item, jz2sfWarn2);
                        addFaults.Add(addFault);
                    }
                }

                if (addFaults.Count > 0)
                {
                    var addnum = _db.Insertable(addFaults).ExecuteCommand();
                    if (addnum > 0)
                    {
                        await Console.Out.WriteLineAsync($"温度传感器异常预警同步完成，新增了{addnum}条预警");
                    }

                    var faultReq = await FaultSetHttpPost(addFaults);
                    if (faultReq != null && faultReq.code == 200)
                    {
                        await Console.Out.WriteLineAsync($"温度传感器异常预警推送成功，新增了{addFaults.Count}条预警");
                    }

                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"温度异常预警失败：{ex.ToString()}");
            }
            
        }
        
        /// <summary>
        /// 制冷系统压力异常预警模型
        /// </summary>
        /// <returns></returns>
        private async Task GetZlxtylFault()
        {
            OVERHAULIDEA jz1Warn1, jz1Warn2, jz2Warn1, jz2Warn2;
            var addFaults = new List<FAULTWARN>();

            var config = _db.Queryable<SYS_CONFIG>().ToList();
            var nowData = _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToList();
            var equipments = await _db.Queryable<OVERHAULIDEA>().ToListAsync();
            var faultData = _db.Queryable<FAULTWARN>().ToList();       

            string sql = @"SELECT * FROM
							    dbo.TB_PARSING_DATAS_YJ_2" + $"_{DateTime.Now:yyyyMMdd} " +
                         @"WHERE
								create_time >= DATEADD(MINUTE,-5,GETDATE())";

            var newDatas = await _db.SqlQueryable<TB_PARSING_DATAS_YJ_2>(sql).ToListAsync();
            if (newDatas.Count == 0) return;

            var jz1ysj1zt = !newDatas.Any(x => x.jz1ysj1yx == "0");
            var jz1ysj2zt = !newDatas.Any(x => x.jz1ysj2yx == "0");
            var jz2ysj1zt = !newDatas.Any(x => x.jz2ysj1yx == "0");
            var jz2ysj2zt = !newDatas.Any(x => x.jz2ysj2yx == "0");
            
            jz1Warn1 = equipments.First(x => x.faultcode == "HVAC02L0110");
            jz1Warn2 = equipments.First(x => x.faultcode == "HVAC02L0120");

            jz2Warn1 = equipments.First(x => x.faultcode == "HVAC02L0210");
            jz2Warn2 = equipments.First(x => x.faultcode == "HVAC02L0220");

            var time = DateTime.Now.AddMinutes(-3);

            var data3minute = newDatas.Where(x => x.create_time >= time);

            foreach (var item in nowData)
            {           
                var ysjdata = data3minute.Where(x => x.device_code == item.device_code);

                if (!ysjdata.Any()) continue;
              
                var jz1ysj1 = ysjdata.Any(x => (Convert.ToInt32(x.jz1zlxt1gyylz) > 2900 && x.jz1zlxt1gycgqgz == "0")|| (Convert.ToInt32(x.jz1zlxt1dyylz) <= 150 && x.jz1zlxt1dycgqgz == "0"));
                var jz1ysj2 = ysjdata.Any(x => (Convert.ToInt32(x.jz1zlxt2gyylz) > 2900 && x.jz1zlxt2gycgqgz == "0")|| (Convert.ToInt32(x.jz1zlxt2dyylz) <= 150 && x.jz1zlxt2dycgqgz == "0"));
                var jz2ysj1 = ysjdata.Any(x => (Convert.ToInt32(x.jz2zlxt1gyylz) > 2900 && x.jz2zlxt1gycgqgz == "0")|| (Convert.ToInt32(x.jz2zlxt1dyylz) <= 150 && x.jz2zlxt1dycgqgz == "0"));
                var jz2ysj2 = ysjdata.Any(x => (Convert.ToInt32(x.jz2zlxt2gyylz) > 2900 && x.jz2zlxt2gycgqgz == "0")|| (Convert.ToInt32(x.jz2zlxt2dyylz) <= 150 && x.jz2zlxt2dycgqgz == "0"));
                
                var jz1w1F = !faultData.Any(x => x.xdid == jz1Warn1.id && x.state == "0" && x.sbbm == item.device_code);
                var jz1w2F = !faultData.Any(x => x.xdid == jz1Warn2.id && x.state == "0" && x.sbbm == item.device_code);

                var jz2w1F = !faultData.Any(x => x.xdid == jz2Warn1.id && x.state == "0" && x.sbbm == item.device_code);
                var jz2w2F = !faultData.Any(x => x.xdid == jz2Warn2.id && x.state == "0" && x.sbbm == item.device_code);

                if (jz1ysj1zt && jz1ysj1 && jz1w1F)
                {
                    var addFault = GetFAULTWARN(item, jz1Warn1);
                    addFaults.Add(addFault);
                }
                if (jz1ysj2zt && jz1ysj2 && jz1w2F)
                {
                    var addFault = GetFAULTWARN(item, jz1Warn2);
                    addFaults.Add(addFault);
                }
                if (jz2ysj1zt && jz2ysj1 && jz2w1F)
                {
                    var addFault = GetFAULTWARN(item, jz2Warn1);
                    addFaults.Add(addFault);
                }
                if (jz2ysj2zt && jz2ysj2 && jz2w2F)
                {
                    var addFault = GetFAULTWARN(item, jz2Warn2);
                    addFaults.Add(addFault);
                }
            }

            if (addFaults.Count > 0)
            {
                var addnum = _db.Insertable(addFaults).ExecuteCommand();
                if (addnum > 0) 
                    await Console.Out.WriteLineAsync($"压缩机异常预警同步完成，新增了{addnum}条预警");

                var faultReq = await FaultSetHttpPost(addFaults);

                if (faultReq != null && faultReq.code == 200)
                    await Console.Out.WriteLineAsync($"压缩机异常预警推送成功，新增了{addFaults.Count}条预警");
            }
        }

        //#region 寿命预警模型

        /// <summary>
        /// 制冷目标温度异常预警模型
        /// </summary>
        /// <returns></returns>
        private async Task GetZlmbwdFault()
        {
            try
            {
                var addFaults = new List<FAULTWARN>();
                var nowData = await _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToListAsync();
                var equipments = await _db.Queryable<OVERHAULIDEA>().ToListAsync();
      
                var Warn1 = equipments.First(x => x.faultcode == "HVAC01L0100");
                var Warn2 = equipments.First(x => x.faultcode == "HVAC01L0200");
                var faultData = _db.Queryable<FAULTWARN>().Where(x => x.state == "0" && x.xdid == Warn1.id).ToList();

                string sql = @"SELECT * FROM
							    dbo.TB_PARSING_DATAS_YJ_2" + $"_{DateTime.Now:yyyyMMdd} " +
                         @"WHERE
								create_time >= DATEADD(MINUTE,-30,GETDATE())";

                var newDatas = await _db.SqlQueryable<TB_PARSING_DATAS_YJ_2>(sql).ToListAsync();
                if (newDatas.Count == 0) return;

                var jz1ysjzt = !newDatas.Any(x => x.jz1ysj1yx == "0" && x.jz1ysj2yx == "0");//压缩机是否存在停机状态
                var jz2ysjzt = !newDatas.Any(x => x.jz2ysj1yx == "0" && x.jz2ysj2yx == "0");//
                
                var time = DateTime.Now.AddMinutes(-5);

                foreach (var item in nowData)
                {
                    var isAny1 = faultData.Any(x => x.sbbm == item.device_code);
                    if (isAny1) continue;

                    var ysjdata = newDatas.Where(x => x.device_code == item.device_code && x.create_time >= time);
                    var newData = newDatas.Where(x => x.device_code == item.device_code);

                    var isTrue5 = ysjdata.Any();
                    var isTrue6 = newData.Any();

                    var isTrue1 = !ysjdata.Any(x => (Convert.ToDouble(x.jz1shfwd) - Convert.ToDouble(x.jz1mbwd)) <= 5) && isTrue5;
                    var isTrue2 = !ysjdata.Any(x => (Convert.ToDouble(x.jz2shfwd) - Convert.ToDouble(x.jz2mbwd)) <= 5) && isTrue5;

                    var isTrue3 = !newData.Any(x => (Convert.ToDouble(x.jz1shfwd) - Convert.ToDouble(x.jz1mbwd)) <= 3) && isTrue6 ;
                    var isTrue4 = !newData.Any(x => (Convert.ToDouble(x.jz2shfwd) - Convert.ToDouble(x.jz2mbwd)) <= 3) && isTrue6;
                   
                    if ((jz1ysjzt && (isTrue1 || isTrue3)) || (jz2ysjzt && (isTrue2 || isTrue4)))
                    {
                        var addFault = GetFAULTWARN(item, Warn1);
                        addFaults.Add(addFault);
                    }
                }

                if (addFaults.Count > 0)
                {
                    var addnum = _db.Insertable(addFaults).ExecuteCommand();
                    if (addnum>0)
                        Console.WriteLine($"制冷目标温度异常预警同步完成，新增了{addnum}条预警");

                    var faultReq = await FaultSetHttpPost(addFaults);
                    if (faultReq != null && faultReq.code == 200)
                        Console.WriteLine($"制冷目标温度异常预警推送成功，新增了{addFaults.Count}条预警");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"制冷目标温度异常预警失败：{ex.ToString()}");
            }
        }
        #region 设备电流异常预警模型

        /// <summary>
        /// 设备电流异常预警模型
        /// </summary>
        /// <returns></returns>
        private async Task GetSbdlFault()
        {
            try
            {
                var addFaults = new List<FAULTWARN>();
                var nowData = await _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToListAsync();
                bool jz1ys1avg = false, jz1ys2avg = false, jz2ys1avg = false, jz2ys2avg = false;


                var time = DateTime.Now;
                var today = DateTime.Today;

                string sql = @"SELECT * FROM
							    dbo.TB_PARSING_DATAS_YJ_2" + $"_{DateTime.Now:yyyyMMdd} " +
                         @"WHERE
								create_time >= DATEADD(MINUTE,-3,GETDATE())";

                var newDatas = await _db.SqlQueryable<TB_PARSING_DATAS_YJ_2>(sql).ToListAsync();
                if (newDatas.Count == 0) return;

                foreach (var item in nowData)
                {
                    var data = newDatas.Where(x => x.device_code == item.device_code).OrderBy(x => x.rqDateTime).ToList();
                    if (data.Count == 0) continue;

                    var ysjAvg = data.Where(x => x.jz1gzms == "6" && x.jz1ysj1pl == x.jz1ysj2pl && x.jz1ysj2pl == x.jz2ysj1pl && x.jz2ysj1pl == x.jz2ysj2pl);

                    foreach (var avg in ysjAvg)
                    {
                        if (jz1ys1avg == false)
                        {
                            var jz1ysj1dl = CalculateAverage(avg.jz1ysj1dl, avg.jz1ysj2dl, avg.jz2ysj1dl, avg.jz2ysj2dl);
                            var jz1ysj1gy = CalculateAverage(avg.jz1zlxt1gyylz, avg.jz1zlxt2gyylz, avg.jz2zlxt1gyylz, avg.jz2zlxt2gyylz);
                            var jz1ysj1dy = CalculateAverage(avg.jz1zlxt1dyylz, avg.jz1zlxt2dyylz, avg.jz2zlxt1dyylz, avg.jz2zlxt2dyylz);

                            if ((jz1ysj1dl && jz1ysj1gy) || (jz1ysj1dl && jz1ysj1dy) || (jz1ysj1gy && jz1ysj1dy))
                            {
                                jz1ys1avg = true;
                            }
                        }

                        if (jz1ys2avg == false)
                        {
                            var jz1ysj1dl = CalculateAverage(avg.jz1ysj2dl, avg.jz1ysj1dl, avg.jz2ysj1dl, avg.jz2ysj2dl);
                            var jz1ysj1gy = CalculateAverage(avg.jz1zlxt2gyylz, avg.jz1zlxt1gyylz, avg.jz2zlxt1gyylz, avg.jz2zlxt2gyylz);
                            var jz1ysj1dy = CalculateAverage(avg.jz1zlxt2dyylz, avg.jz1zlxt1dyylz, avg.jz2zlxt1dyylz, avg.jz2zlxt2dyylz);

                            if ((jz1ysj1dl && jz1ysj1gy) || (jz1ysj1dl && jz1ysj1dy) || (jz1ysj1gy && jz1ysj1dy))
                            {
                                jz1ys2avg = true;
                            }
                        }

                        if (jz2ys1avg == false)
                        {
                            var jz1ysj1dl = CalculateAverage(avg.jz2ysj1dl, avg.jz1ysj1dl, avg.jz1ysj2dl, avg.jz2ysj2dl);
                            var jz1ysj1gy = CalculateAverage(avg.jz2zlxt1gyylz, avg.jz1zlxt1gyylz, avg.jz1zlxt2gyylz, avg.jz2zlxt2gyylz);
                            var jz1ysj1dy = CalculateAverage(avg.jz2zlxt1dyylz, avg.jz1zlxt1dyylz, avg.jz1zlxt2dyylz, avg.jz2zlxt2dyylz);

                            if ((jz1ysj1dl && jz1ysj1gy) || (jz1ysj1dl && jz1ysj1dy) || (jz1ysj1gy && jz1ysj1dy))
                            {
                                jz2ys1avg = true;
                            }
                        }

                        if (jz2ys2avg == false)
                        {
                            var jz1ysj1dl = CalculateAverage(avg.jz2ysj2dl, avg.jz1ysj1dl, avg.jz1ysj2dl, avg.jz2ysj1dl);
                            var jz1ysj1gy = CalculateAverage(avg.jz2zlxt2gyylz, avg.jz1zlxt1gyylz, avg.jz1zlxt2gyylz, avg.jz2zlxt1gyylz);
                            var jz1ysj1dy = CalculateAverage(avg.jz2zlxt2dyylz, avg.jz1zlxt1dyylz, avg.jz1zlxt2dyylz, avg.jz2zlxt1dyylz);

                            if ((jz1ysj1dl && jz1ysj1gy) || (jz1ysj1dl && jz1ysj1dy) || (jz1ysj1gy && jz1ysj1dy))
                            {
                                jz2ys2avg = true;
                            }
                        }

                        if (jz1ys1avg && jz1ys2avg && jz2ys1avg && jz2ys2avg)
                        {
                            break;
                        }
                    }
                    
                    var jz1zffj1 = HasConsecutiveFaults(data, Jz1zffj1yx, Jz1zffj1dl, 60, ZFFJ_I);
                    var jz1zffj2 = HasConsecutiveFaults(data, Jz1zffj2yx, Jz1zffj2dl, 60, ZFFJ_I);
                    var jz2zffj1 = HasConsecutiveFaults(data, Jz2zffj1yx, Jz2zffj1dl, 60, ZFFJ_I);
                    var jz2zffj2 = HasConsecutiveFaults(data, Jz2zffj2yx, Jz2zffj2dl, 60, ZFFJ_I);

                    var jz1lnfj1 = HasConsecutiveFaults(data, Jz1lnfj1yx, Jz1lnfj1dl, 60, LNFJ_I);
                    var jz1lnfj2 = HasConsecutiveFaults(data, Jz1lnfj2yx, Jz1lnfj2dl, 60, LNFJ_I);
                    var jz2lnfj1 = HasConsecutiveFaults(data, Jz2lnfj1yx, Jz2lnfj1dl, 60, LNFJ_I);
                    var jz2lnfj2 = HasConsecutiveFaults(data, Jz2lnfj2yx, Jz2lnfj2dl, 60, LNFJ_I);

                    var jz1ysj1 = HasConsecutiveFaults(data, Jz1ysj1yx, Jz1ysj1dl, 60 * 3, YSJ_I);
                    var jz1ysj2 = HasConsecutiveFaults(data, Jz1ysj2yx, Jz1ysj2dl, 60 * 3, YSJ_I);
                    var jz2ysj1 = HasConsecutiveFaults(data, Jz2ysj1yx, Jz2ysj1dl, 60 * 3, YSJ_I);
                    var jz2ysj2 = HasConsecutiveFaults(data, Jz2ysj2yx, Jz2ysj2dl, 60 * 3, YSJ_I);

                    if (jz1zffj1)
                    {
                        var addFault = await GetFAULTWARN("HVAC08L0110",item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                    if (jz1zffj2)
                    {
                        var addFault = await GetFAULTWARN("HVAC08L0120", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                    if (jz2zffj1)
                    {
                        var addFault = await GetFAULTWARN("HVAC08L0210", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                    if (jz2zffj2)
                    {     
                        var addFault = await GetFAULTWARN("HVAC08L0220", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }

                    if (jz1lnfj1)
                    {
                        var addFault = await GetFAULTWARN("HVAC09L0110", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                    if (jz1lnfj2)
                    {
                        var addFault = await GetFAULTWARN("HVAC09L0120", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                    if (jz2lnfj1)
                    {
                        var addFault = await GetFAULTWARN("HVAC09L0210", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                    if (jz2lnfj2)
                    {
                        var addFault = await GetFAULTWARN("HVAC09L0220", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }

                    if (jz1ysj1 || jz1ys1avg)
                    {
                        var addFault = await GetFAULTWARN("HVAC10L0110", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                    if (jz1ysj2 || jz1ys2avg)
                    {
                        var addFault = await GetFAULTWARN("HVAC10L0120", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                    if (jz2ysj1 || jz2ys1avg)
                    {
                        var addFault = await GetFAULTWARN("HVAC10L0210", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                    if (jz2ysj2 || jz2ys2avg)
                    {
                        var addFault = await GetFAULTWARN("HVAC10L0220", item, today);
                        if (addFault != null) addFaults.Add(addFault);
                    }
                }

                if (addFaults.Count > 0)
                {
                    var addnum = _db.Insertable(addFaults).ExecuteCommand();
                    if (addnum > 0)
                        Console.WriteLine($"制冷目标温度异常预警同步完成，新增了{addnum}条预警");

                    var faultReq = await FaultSetHttpPost(addFaults);
                    if (faultReq != null && faultReq.code == 200)
                        Console.WriteLine($"制冷目标温度异常预警推送成功，新增了{addFaults.Count}条预警");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"制冷目标温度异常预警失败：{ex.ToString()}");
            }
        }
      
        /// <summary>
        /// 压缩机指标是否超过平均值
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <returns></returns>
        private static bool CalculateAverage(string value, string value1, string value2, string value3)
        {
            var avg = (Convert.ToDouble(value1) + Convert.ToDouble(value2) + Convert.ToDouble(value3)) / 3;
            var max = avg + avg * 0.1;
            var min = avg - avg * 0.1;
            return Convert.ToDouble(value) > max || Convert.ToDouble(value) < min;
        }

        /// <summary>
        /// 添加故障
        /// </summary>
        /// <param name="faultCode"></param>
        /// <param name="item"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private async Task<FAULTWARN?> GetFAULTWARN(string faultCode, TB_PARSING_DATAS_NEWCS item,DateTime time)
        {
            var equipments = await _db.Queryable<OVERHAULIDEA>().ToListAsync();
            var faultData = await _db.Queryable<FAULTWARN>().Where(x => x.state == "0").ToListAsync();

            var Warn = equipments.First(x => x.faultcode == faultCode);
            var F = !faultData.Any(x => x.xdid == Warn.id && x.sbbm == item.device_code && x.createtime > time);
            if (F)
            {
                return GetFAULTWARN(item, Warn);               
            }
            return null;
        }


        static bool Jz1lnfj1yx(TB_PARSING_DATAS_YJ_2 x) => x.jz1lnfj1yx == "0";
        static bool Jz1lnfj2yx(TB_PARSING_DATAS_YJ_2 x) => x.jz1lnfj2yx == "0";
        static bool Jz2lnfj1yx(TB_PARSING_DATAS_YJ_2 x) => x.jz2lnfj1yx == "0";
        static bool Jz2lnfj2yx(TB_PARSING_DATAS_YJ_2 x) => x.jz2lnfj2yx == "0";

        static bool Jz1zffj1yx(TB_PARSING_DATAS_YJ_2 x) => x.jz1zffj1yx == "0";
        static bool Jz1zffj2yx(TB_PARSING_DATAS_YJ_2 x) => x.jz1zffj2yx == "0";
        static bool Jz2zffj1yx(TB_PARSING_DATAS_YJ_2 x) => x.jz2zffj1yx == "0";
        static bool Jz2zffj2yx(TB_PARSING_DATAS_YJ_2 x) => x.jz2zffj2yx == "0";

        static bool Jz1ysj1yx(TB_PARSING_DATAS_YJ_2 x) => x.jz1ysj1yx == "0";
        static bool Jz1ysj2yx(TB_PARSING_DATAS_YJ_2 x) => x.jz1ysj2yx == "0";
        static bool Jz2ysj1yx(TB_PARSING_DATAS_YJ_2 x) => x.jz2ysj1yx == "0";
        static bool Jz2ysj2yx(TB_PARSING_DATAS_YJ_2 x) => x.jz2ysj2yx == "0";

        static double Jz1zffj1dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz1zffj1yx);
        static double Jz1zffj2dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz1zffj2yx);
        static double Jz2zffj1dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz2zffj1yx);
        static double Jz2zffj2dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz2zffj2yx);

        static double Jz1lnfj1dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz1lnfj1dl);
        static double Jz1lnfj2dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz1lnfj2dl);
        static double Jz2lnfj1dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz2lnfj1dl);
        static double Jz2lnfj2dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz2lnfj2dl);

        static double Jz1ysj1dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz1ysj1dl);
        static double Jz1ysj2dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz1ysj2dl);
        static double Jz2ysj1dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz2ysj1dl);
        static double Jz2ysj2dl(TB_PARSING_DATAS_YJ_2 x) => Convert.ToDouble(x.jz2ysj2dl);


        static bool ZFFJ_I(double I) => I >= 0.8 && I <= 2.3;
        static bool LNFJ_I(double I) => I >= 1.8 && I <= 2.7;
        static bool YSJ_I(double I) => I >= 9 && I <= 22;
   
        //定义一个委托
        public delegate bool SampleCheck(double sample);

        // 检查列表中是否有连续n个数据异常
        public bool HasConsecutiveFaults<T>(List<T> data, Func<T, bool> yxzt, Func<T, double> selector, int n, SampleCheck check)
        {
            if (data == null || n <= 0)
            {
                _logger.LogError("预警失败，数据不符合预警条件（空集合或n小于等于0）。");
                return false;
            }
            var any = data.Any(yxzt);

            if (any) return false;

            var samples = data.Select(selector).ToList();
            if (n > samples.Count)
            {
                _logger.LogError("预警失败，n大于数据集中的元素数量。");
                return false;
            }

            for (int i = 0; i <= samples.Count - n; i++)
            {
                bool allFaulty = true;
                for (int j = 0; j < n; j++)
                {
                    if (!check(samples[i + j]))
                    {
                        allFaulty = false;
                        break;
                    }
                }
                if (allFaulty)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        /// <summary>
        /// 寿命预警模型
        /// </summary>
        /// <returns></returns>
        private async Task GetSmFault()
        {
            try
            {
                var addFaults = new List<FAULTWARN>();
                var devList = await _db.Queryable<DEVICE>().ToListAsync();
                var devs = await _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToListAsync();
                var partFaults = await _db.Queryable<DEVIMGDB>().Where(x => !string.IsNullOrEmpty(x.faultcode)).ToListAsync();
                var DEVPARTS = await _db.Queryable<DEVPARTS>().ToListAsync();
                var faultData = await _db.Queryable<FAULTWARN>().Where(x => x.state == "0").ToListAsync();
                var equipments = await _db.Queryable<OVERHAULIDEA>().ToListAsync();
                var time = DateTime.Today;

                foreach (var item in partFaults)
                {
                    var devParts = DEVPARTS.Where(x => x.lbjid == item.id).ToList();
                    var eq = equipments.First(x => x.faultcode == item.faultcode);
                    
                    foreach (var devp in devParts)
                    {
                        var dev = devList.First(x => x.id == devp.sbid);
                        var dev1 = devs.First(x => x.device_code == dev.device_id);
                        var fault = !faultData.Any(x => x.sbid == devp.sbid && x.xdid == eq.id && x.createtime > time);
                        var lifeT = devp.servicelife > item.ratedlife * 0.9;
                        if (fault && lifeT)
                        {
                            var addFault = GetFAULTWARN(dev1, eq);
                            addFaults.Add(addFault);
                        }
                    }
                }

                if (addFaults.Count > 0)
                {
                    var addnum = _db.Insertable(addFaults).ExecuteCommand();
                    if (addnum > 0)
                        Console.WriteLine($"寿命预警同步完成，新增了{addnum}条预警");

                    var faultReq = await FaultSetHttpPost(addFaults);
                    if (faultReq != null && faultReq.code == 200)
                    {
                        Console.WriteLine($"寿命警推送成功，推送了{addFaults.Count}条预警");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"寿命预警失败：{ex.ToString()}");
            }
        }

        //#endregion

        private async Task<List<WarnDTO>> GetNewData(string? time)
        {
            var config = _db.Queryable<SYS_CONFIG>().ToList();
            //获取故障时间
            var sj = config.Where(x => x.concode == "WarnSj").First()?.conval;

            if (!string.IsNullOrEmpty(time))
            {
                sj = time;
            }
            string sql = @"SELECT * FROM
							    dbo.TB_PARSING_DATAS" + $"_{DateTime.Now:yyyyMMdd} "+
                         @"WHERE
								yj.create_time >= DATEADD(MINUTE,-{0},GETDATE())";

            var faults_sql = string.Format(sql, sj);
            //获取实时故障数据
            var faults = await _db.SqlQueryable<WarnDTO>(faults_sql).ToListAsync();

            return faults;
        }

        /// <summary>
        /// 预警初始化
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="fault"></param>
        /// <returns></returns>
        private  FAULTWARN GetFAULTWARN (TB_PARSING_DATAS_NEWCS dev, OVERHAULIDEA fault)
        {
            var lch = _db.Queryable<LCH>().First(x => x.lch == dev.lch);
            var cxh = _db.Queryable<CXH>().First(x => x.lcid == lch.id && x.cxh == dev.cxh);
          
            var addFault = new FAULTWARN
            {
                line_id = Convert.ToInt32(lch.xl_id),
                lch = dev.lch,
                lcid = lch.id,
                cxid = cxh.id,
                cxh = dev.cxh,
                sbid = (int?)dev.id,
                sbbm = dev.device_code,
                state = "0",
                collect_time = DateTime.Now,
                createtime = DateTime.Now,
                xdid = fault.id,
                type = fault.type,
                xdmc = fault.jxname,
                gzjb = fault.gzdj,
                sfxztb = Guid.NewGuid().ToString("N")
            };

            return addFault;
        }

        #endregion

        //#region 物模型上传

        //public async Task XieruKafkaAsync()
        //{
        //    await Console.Out.WriteLineAsync($"物模型上传中......");
        //    try
        //    {

        //        var trainSta = await GetTrainSta();
        //        var onlien = trainSta?.result_data.FirstOrDefault()?.online_trains?.ToList();
        //        var depot = trainSta?.result_data.FirstOrDefault()?.depot_trains?.ToList();
        //        onlien.AddRange(depot);

        //        // 获取实时信息
        //        var dt = _db.Queryable<TB_PARSING_NEWDATAS>().Where(x => onlien.Contains(x.lch)).ToList();

        //        var trains = new TrainData();
        //        // 按列车分组
        //        var groupedByTrain = dt.GroupBy(row => row.lch);
        //        foreach (var trainGroup in groupedByTrain)
        //        {
        //            var trainData = new TrainData
        //            {
        //                lineCode = "GZML7S",
        //                trainCode = trainGroup.Key,
        //                systemCode = "HVAC",
        //                createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        //                data = new Dictionary<string, Car>()
        //            };
        //            // 按车厢分组
        //            var groupedByCar = trainGroup.GroupBy(row => row.cxh);
        //            foreach (var carGroup in groupedByCar)
        //            {
        //                string device_id = carGroup.Key;
        //                var car = new Car(); // 初始化车厢
        //                foreach (var item in carGroup)
        //                {
        //                    var HVAC = new dynamicProperties
        //                    {
        //                        freshairTemp = item.jz1swwd,
        //                        supplyairTemp = item.jz1sfcgq1wd,
        //                        returnairTemp = item.jz1kswd,
        //                        carairTemp = item.jz1kswd,
        //                        airqualitycollectionmoduleTemp = item.jz1kqzljcmkwd,
        //                        airqualitycollectionmoduleRH = item.kssdz,
        //                        airqualitycollectionmoduleCO2 = item.jz1co2nd,
        //                        airqualitycollectionmoduleTVOC = item.jz1tvocnd,
        //                        airqualitycollectionmodulePM = item.jz1pm2d5nd,
        //                        airPressureDifference = 1,
        //                        targetTemp = item.jz1mbwd,
        //                        voltage = 220,
        //                        Maincircuitbreaker = item.jz1zhl1dlqgz,
        //                        compressorcircuitbreaker = item.jz1ysj1dlqgz,
        //                        inverter = item.jz1bpq1gz,
        //                        UVlampmalfunction = item.jz1zwxd1gz,
        //                        returnairdamper = item.jz1hff1gz,
        //                        freshairdamper = item.jz1xff1gz,
        //                        aircleaner = item.jz1kqjhqgz,
        //                        lowpressureswitch = item.jz1dyylcgq1gz,
        //                        highpressureswitch = item.jz1gyylcgq1gz,
        //                        severeFault = item.jz1yzgz,
        //                        midFault = item.jz1zdgz,
        //                        minorFault = item.jz1qwgz
        //                    };

        //                    var COM01 = new CompressordynamicProperties
        //                    {
        //                        exhaustTemp = item.jz1ysj1pqwd,
        //                        suctionTemp = item.jz1ysj1xqwd,
        //                        highPressure = item.jz1ysj1gyyl,
        //                        lowPressure = item.jz1ysj1dyyl,
        //                        compressorId = item.jz1ysj1vxdlz,
        //                        contactorStatus = item.jz1ysj1jcqgz
        //                    };

        //                    var COM02 = new CompressordynamicProperties
        //                    {
        //                        exhaustTemp = item.jz1ysj2pqwd,
        //                        suctionTemp = item.jz1ysj2xqwd,
        //                        highPressure = item.jz1ysj2gyyl,
        //                        lowPressure = item.jz1ysj2dyyl,
        //                        compressorId = item.jz1ysj2vxdlz,
        //                        contactorStatus = item.jz1ysj2jcqgz
        //                    };

        //                    var EVP01 = new VentilationdynamicProperties
        //                    {
        //                        blowerId = item.jz1tfj1uxdlz,
        //                        ventilationcontactorStatus = item.jz1tfj1gzgz,
        //                        emergencyventilationcontactorStatus = item.jz1tfjjjtfjcqgz,
        //                    };

        //                    var EVP02 = new VentilationdynamicProperties
        //                    {
        //                        blowerId = item.jz1tfj2uxdlz,
        //                        ventilationcontactorStatus = item.jz1tfj2gzgz,
        //                        emergencyventilationcontactorStatus = item.jz1tfjjjtfjcqgz,
        //                    };

        //                    var WEX01 = new ExhaustdynamicProperties
        //                    {
        //                        fanId = item.jz1lnfj1uxdlz,
        //                        emergencyventilationcontactorStatus = item.jz1lnfj1gzgz,
        //                    };

        //                    var WEX02 = new ExhaustdynamicProperties
        //                    {
        //                        fanId = item.jz1lnfj2uxdlz,
        //                        emergencyventilationcontactorStatus = item.jz1lnfj2gzgz,
        //                    };

        //                    if (item.yxtzjid == 1)
        //                    {
        //                        car.HVAC01 = new HVACUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = HVAC
        //                        };

        //                        // 填充HVAC01COM02
        //                        car.HVAC01COM01 = new CompressorUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = COM01
        //                        };

        //                        // 填充HVAC02COM01
        //                        car.HVAC01COM02 = new CompressorUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = COM02
        //                        };

        //                        // 填充HVAC01EVP01
        //                        car.HVAC01EVP01 = new VentilationUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = EVP01
        //                        };

        //                        // 填充HVAC01EVP02
        //                        car.HVAC01EVP02 = new VentilationUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = EVP02
        //                        };

        //                        // 填充HVAC01WEX01
        //                        car.HVAC01WEX01 = new ExhaustUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = WEX01
        //                        };

        //                        // 填充HVAC01WEX02
        //                        car.HVAC01WEX02 = new ExhaustUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = WEX02
        //                        };
        //                    }

        //                    if (item.yxtzjid == 2)
        //                    {
        //                        car.HVAC02 = new HVACUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = HVAC
        //                        };

        //                        car.HVAC02COM01 = new CompressorUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = COM01
        //                        };

        //                        car.HVAC02COM02 = new CompressorUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = COM02
        //                        };

        //                        car.HVAC02EVP01 = new VentilationUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = EVP01
        //                        };

        //                        car.HVAC02EVP02 = new VentilationUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = EVP02
        //                        };

        //                        car.HVAC02WEX01 = new ExhaustUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = WEX01
        //                        };

        //                        car.HVAC02WEX02 = new ExhaustUnit
        //                        {
        //                            designProperties = new Dictionary<string, string>(),
        //                            dynamicProperties = WEX02
        //                        };
        //                    }
        //                }

        //                trainData.data.Add(device_id, car);
        //            }
        //            var aa = JsonConvert.SerializeObject(trainData);
        //            var config = new ProducerConfig
        //            {
        //                BootstrapServers = _appSettings.KafkaConfig.bootstrapServers,
        //                ClientId = Dns.GetHostName()// 客户端ID
        //            };
        //            CancellationTokenSource cts = new CancellationTokenSource();
        //            // 获取 CancellationToken
        //            CancellationToken ct = cts.Token;
        //            var producerBuilder = new ProducerBuilder<Null, string>(config);
        //            IProducer<Null, string> producer = producerBuilder.Build();
        //            string topic = "gzml7s-hvacmodel"; // 设置目标主题 gzml7s-hvacmodel   test_0524
        //            var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = aa }, ct);
        //            await Console.Out.WriteLineAsync($"物模型上传成功");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Console.Out.WriteLineAsync($"物模型上传失败：" + ex.Message);
        //        _logger.LogDebug($"物模型上传失败" + ex.Message);
        //        //  return JsonConvert.SerializeObject(new { error = ex.Message });
        //    }
        //}

        ///// <summary>
        ///// 获取车辆在线信息
        ///// </summary>
        ///// <returns></returns>
        //public async Task<HttpTrainStaDTO?> GetTrainSta()
        //{
        //    string urlType = "line-statistics";
        //    // 构建app_token
        //    string appToken = $"app_id={_appId}&app_key={_appKey}&date=" + DateTime.Now.ToString("yyyy-MM-dd");
        //    string tokenMd5 = Helper.GetMD5String(appToken).ToUpper();
        //    string url = $"{_baseUrl}{urlType}?app_id={_appId}&app_token={tokenMd5}&line_code={_lineCode}";
        //    var trainSta = await HttpClientExample.SendGetRequestAsync<HttpTrainStaDTO>(url);
        //    return trainSta;
        //}

        //#endregion
    }
}





