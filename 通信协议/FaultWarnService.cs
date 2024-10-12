using AutoMapper;
using Confluent.Kafka;
using DataBase.DTO;
using DataBase.Tables;
using KP.Util;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SqlSugar;
using System.Collections.Concurrent;
using System.Data;
using System.Net;
using System.Reflection;
using System.Xml.Linq;

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
                            //await Delete();
                        }
                        catch (TaskCanceledException)
                        {
                            // 计时器被取消，退出循环  
                            break;
                        }
                    }
                });

            }, null, TimeSpan.Zero, TimeSpan.FromDays(1));

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
                //await FaultDataPush();

            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(Convert.ToDouble(_FaultDataPush)));

            _lifetTimer = new Timer(async _ =>
            {
                _logger.LogInformation("开始更新寿命");

                await AddOrUpdateSMDataV1();

            }, null, TimeSpan.Zero, TimeSpan.FromHours(Convert.ToDouble(_AddOrUpdateSMData)));

            _warnTimer = new Timer(async _ =>
            {
                _logger.LogInformation("开始预警");

                try
                {
                    //await GetSfwdFault();
                   
                    //await GetZlmbwdFault();
                    //await GetZlxtylFault();
                    //await GetZwxdsmFault();
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
            await Task.CompletedTask;
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

                var sql2 = $@"if object_id('TB_PARSING_DATAS_YJ_1_{date1}','U') is not null 
                           drop table  TB_PARSING_DATAS_YJ_1_{date1}";

                var sql3 = $@"if object_id('TB_PARSING_DATAS_YJ_2_{date1}','U') is not null 
                           drop table  TB_PARSING_DATAS_YJ_2_{date1}";

                var ysbwSql = $@"if object_id('TB_YSBW_{date30}','U') is not null 
                           drop table TB_YSBW_{date30}";

                //删除过期数据
                await _db.Ado.ExecuteCommandAsync(sql);
                await _db.Ado.ExecuteCommandAsync(sql1);
                await _db.Ado.ExecuteCommandAsync(sql2);
                await _db.Ado.ExecuteCommandAsync(sql3);
                await _db.Ado.ExecuteCommandAsync(ysbwSql);

                _logger.LogError("删除成功");
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
                            "jz1zwxd" => (d.id, 134),
                            "jz2zwxd" => (d.id, 135),
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
	                                gz.jz2lnfj2gzyj, 
	                                gz.jz2lnfj1gzyj, 
	                                gz.jz1lnfj2gzyj, 
	                                gz.jz1lnfj1gzyj, 
	                                gz.jz2zffj2gzyj, 
	                                gz.jz2zffj1gzyj, 
	                                gz.jz1zffj2gzyj, 
	                                gz.jz1zffj1gzyj, 
	                                gz.jz2ysj2smyj, 
	                                gz.jz2ysj1smyj, 
	                                gz.jz1ysj2smyj, 
	                                gz.jz1ysj1smyj, 
	                                gz.jz2ysj2gzyj, 
	                                gz.jz2ysj1gzyj, 
	                                gz.jz1ysj2gzyj, 
	                                gz.jz1ysj1gzyj, 
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
                var equipments = await _db.Queryable<OVERHAULIDEA>().Where(x => !string.IsNullOrEmpty(x.gzval) && x.type == "3").ToListAsync();
                var faultData = _db.Queryable<FAULTWARN>().ToList();
                var devicData = _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToList();

                var lchs = _db.Queryable<LCH>().ToList();
                var cxhs = _db.Queryable<CXH>().ToList();
                //获取故障数据
                var faults = await _db.SqlQueryable<FaultDTO>(sql).OrderBy(x => x.rq).ToListAsync();

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
                            createtime = DateTime.Now
                        };
                        addFaults.Add(faultOrWarn);
                    }
                }

                var faultOn = faultData.Where(x => x.state == "0" && x.type == "3").ToList();
                foreach (var On in faultOn)
                {
                    var Aname = equipments.Where(x => x.id == On.xdid).First();
                    if (Aname == null) continue;

                    var newData = faults.Where(x => x.device_code == On.sbbm).OrderByDescending(x =>x.rqDateTime).First();
                    if (newData == null) continue;

                    var dic = GetPropertiesAndValues(newData);
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

                _logger.LogInformation($"故障同步完成，新增了{num}条故障,关闭了{num1}条故障");

            }
            catch (Exception ex)
            {
                _logger.LogError($"故障信息添加失败，{ex.ToString()}");
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

        /// <summary>
        /// 故障推送
        /// </summary>
        /// <param name="newFault"></param>
        /// <param name="endFault"></param>
        /// <returns></returns>
        public async Task<HttpReq<FaultReq>> FaultSetHttpPost(List<FaultOrWarn> newFault, List<FaultOrWarn> endFault)
        {

            string urlType = "fault-assess";

            // 构建app_token
            string appToken = $"app_id={_appId}&app_key={_appKey}&date=" + DateTime.Now.ToString("yyyy-MM-dd");
            string tokenMd5 = Helper.GetMD5String(appToken).ToUpper();
            string url = $"{_baseUrl}{urlType}";

            var new_faults = new List<FaultsModels>();
            var end_faults = new List<FaultsModels>();

            foreach (var item in newFault)
            {
                new_faults.Add(new FaultsModels()
                {
                    fault_name = item.Name,
                    line_code = _lineCode,
                    train_code = item.lch,
                    coach_no = item.cxh.Substring(2),
                    fault_code = item.Code,
                    access_time = item.createtime?.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }

            foreach (var item in endFault)
            {
                end_faults.Add(new FaultsModels()
                {
                    fault_name = item.Name,
                    line_code = _lineCode,
                    train_code = item.lch,
                    fault_code = item.Code,
                    coach_no = item.cxh.Substring(2),
                    access_time = item.updatetime?.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }

            var request = new Fault_AssessModels()
            {
                app_id = _appId,
                app_token = tokenMd5,
                new_faults = new_faults,
                end_faults = end_faults
            };

            var faultReq = await HttpClientExample.SendPostRequestAsync<HttpReq<FaultReq>>(url, request);

            return faultReq;
        }

        #region 预警模型
        /// <summary>
        /// 送风温度异常预警/每分钟预警
        /// </summary>
        /// <returns></returns>
        private async Task GetSfwdFault()
        {          
            var addFaults = new List<FAULTWARN>();
            //var config = _db.Queryable<SYS_CONFIG>().ToList();
            var nowData = await _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToListAsync();
            var equipments = await _db.Queryable<OVERHAULIDEA>().ToListAsync();        

            var faultData = _db.Queryable<FAULTWARN>().Where(x => x.state == "0").ToList();

            string sql = @"SELECT * FROM
							    TB_PARSING_DATAS_YJ_1" + $"_{DateTime.Now:yyyyMMdd} " +
                     @"WHERE
								create_time >= DATEADD(MINUTE,-3,GETDATE())";

            var newDatas = await _db.SqlQueryable<TB_PARSING_DATAS_YJ_2>(sql).ToListAsync();
            if (newDatas.Count == 0) return;

            var jz1sfWarn1 = equipments.First(x => x.id == 269);
            var jz1sfWarn2 = equipments.First(x => x.id == 270);
            var jz2sfWarn1 = equipments.First(x => x.id == 273);
            var jz2sfWarn2 = equipments.First(x => x.id == 274);

            var time = DateTime.Now.AddMinutes(-1);
            var data1mut = newDatas.Where(x => x.create_time >= time).ToList();

            foreach (var item in nowData)
            {
                var jz1sf1fault = !faultData.Any(x => x.xdid == 269 && x.sbid == item.id);
                var jz1sf2fault = !faultData.Any(x => x.xdid == 270 && x.sbid == item.id);
                var jz2sf1fault = !faultData.Any(x => x.xdid == 273 && x.sbid == item.id);
                var jz2sf2fault = !faultData.Any(x => x.xdid == 274 && x.sbid == item.id);

                var data = data1mut.Where(x => x.device_code == item.device_code).ToList();

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
                //var faultReq = await FaultSetHttpPost(addFaults, new List<FaultOrWarn>());

                //if (faultReq != null && faultReq.result_code == "200")
                //{
                //    _logger.LogInformation($"温度异常预警推送成功，新增了{addFaults.Count}条预警");

                //    for (int i = 0; i < addFaults.Count; i++)
                //    {
                //        addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
                //    }
                //}
                var addnum = _db.Insertable(addFaults).ExecuteCommand();
                _logger.LogInformation($"温度异常预警同步完成，新增了{addnum}条预警");
            }
        }
        
        /// <summary>
        /// 制冷系统压力异常预警模型
        /// </summary>
        /// <returns></returns>
        private async Task GetZlxtylFault()
        {
            OVERHAULIDEA jz1Warn1, jz1Warn2, jz2Warn1, jz2Warn2, gyWarn1, gyWarn2, dyWarn1, dyWarn2;
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
            
            jz1Warn1 = equipments.First(x => x.faultcode == "1x07036");
            jz1Warn2 = equipments.First(x => x.faultcode == "1x07037");

            jz2Warn1 = equipments.First(x => x.faultcode == "1x07038");
            jz2Warn2 = equipments.First(x => x.faultcode == "1x07039");

            var time = DateTime.Now.AddMinutes(-3);

            var data3minute = newDatas.Where(x => x.create_time >= time);

            foreach (var item in nowData)
            {           
                var ysjdata = data3minute.Where(x => x.device_code == item.device_code);

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
                //var faultReq = await FaultSetHttpPost(addFaults, new List<FaultOrWarn>());

                //if (faultReq != null && faultReq.result_code == "200")
                //{
                //    _logger.LogInformation($"压缩机异常预警推送成功，新增了{addFaults.Count}条预警");

                //    for (int i = 0; i < addFaults.Count; i++)
                //    {
                //        addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
                //    }
                //}
                var addnum = _db.Insertable(addFaults).ExecuteCommand();
                _logger.LogInformation($"压缩机异常预警同步完成，新增了{addnum}条预警");
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
      

                var Warn1 = equipments.First(x => x.faultcode == "1x07033");
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
                    //var faultReq = await FaultSetHttpPost(addFaults, new List<FaultOrWarn>());

                    //if (faultReq != null && faultReq.result_code == "200")
                    //{
                    //    Console.WriteLine($"制冷目标温度异常预警推送成功，新增了{addFaults.Count}条预警");

                    //    for (int i = 0; i < addFaults.Count; i++)
                    //    {
                    //        addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
                    //    }
                    //}

                    var addnum = _db.Insertable(addFaults).ExecuteCommand();
                    _logger.LogInformation($"制冷目标温度异常预警同步完成，新增了{addnum}条预警");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"制冷目标温度异常预警失败：{ex.ToString()}");
            }
        }

        /// <summary>
        /// 紫外灯寿命预警模型
        /// </summary>
        /// <returns></returns>
        private async Task GetZwxdsmFault()
        {
            try
            {
                var addFaults = new List<FAULTWARN>();
                //var config = _db.Queryable<SYS_CONFIG>().ToList();
                var nowData = await _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToListAsync();
                var equipments = await _db.Queryable<OVERHAULIDEA>().ToListAsync();

                var jz1zwxdF = equipments.First(x => x.id == 1477);
                var jz2zwxdF = equipments.First(x => x.id == 1478);
              
                var time = DateTime.Today;
                var faultData = _db.Queryable<FAULTWARN>().Where(x => x.state == "0" && x.createtime > time).ToList();

                var newDatas = await _db.Queryable<DEVPARTS>().Where(x => x.lbjid == 134 || x.lbjid == 135).ToListAsync();
                if (newDatas.Count == 0) return;

                var jz1zwdrateT = _db.Queryable<DEVIMGDB>().First(x => x.id == 134);
                var jz2zwdrateT = _db.Queryable<DEVIMGDB>().First(x => x.id == 135);

                foreach (var item in nowData)
                {
                    var jz1f1 = !faultData.Any(x => x.xdid == jz1zwxdF.id && x.sbid == item.id);
                    var jz1f2 = !faultData.Any(x => x.xdid == jz2zwxdF.id && x.sbid == item.id);
                 
                    var data1 = newDatas.FirstOrDefault(x => x.sbid == item.id && x.lbjid == 134);
                    var data2 = newDatas.FirstOrDefault(x => x.sbid == item.id && x.lbjid == 135);
                  
                    var life1T = data1.servicelife > (jz1zwdrateT.ratedlife * 0.9);
                    var life2T = data2.servicelife > (jz2zwdrateT.ratedlife * 0.9);

                    if (jz1f1 && life1T)
                    {
                        var addFault1 = GetFAULTWARN(item, jz1zwxdF);
                        addFaults.Add(addFault1);
                    }

                    if (jz1f2 && life2T)
                    {
                        var addFault = GetFAULTWARN(item, jz2zwxdF);
                        addFaults.Add(addFault);
                    }
                }

                //var faultReq = await FaultSetHttpPost(addFaults, new List<FaultOrWarn>());

                //if (faultReq != null && faultReq.result_code == "200")
                //{
                //    _logger.LogInformation($"制冷剂泄露预警推送成功，新增了{addFaults.Count}条预警");

                //    for (int i = 0; i < addFaults.Count; i++)
                //    {
                //        addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
                //    }
                //}

                if (addFaults.Count > 0)
                {
                    var addnum = _db.Insertable(addFaults).ExecuteCommand();
                    _logger.LogInformation($"紫外灯寿命预警同步完成，新增了{addnum}条预警");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"紫外灯寿命预警失败：{ex.ToString()}");
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





