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

            //_lifetTimer = new Timer(async _ =>
            //{
            //    _logger.LogInformation("开始更新寿命并推送");
            //    await AddOrUpdateSMData();
            //    await AddOrUpdateSMDataV1();

            //}, null, TimeSpan.Zero, TimeSpan.FromHours(Convert.ToDouble(_AddOrUpdateSMData)));

            _warnTimer = new Timer(async _ =>
            {
                _logger.LogInformation("开始预警");

                try
                {
                    //await GetWdFault();
                    //await GetYsjFault();
                    //await GetZfqzdFault();
                    //await GetZljxlFault();
                    await GetZlmbwdFault();
                    //await GetSbdlzFault();
                    //await GetLwzdFault();
                    //await GetLnqzdFault();
                    //await GetKqzlFault();
                    //await GetLnjcfdlFault();
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

        ///// <summary>
        ///// 更新寿命数据并推送
        ///// </summary>
        ///// <param name="dataList"></param>
        ///// <returns></returns>
        ///// <exception cref="Exception"></exception>
        //private async Task AddOrUpdateSMData()
        //{
        //    var result = new List<PartsLife>();

        //    try
        //    {
        //        var nowdate = DateTime.Now;
        //        var startime = nowdate.AddMinutes(-5);
        //        var dataTime = DateTime.Now.ToString("yyyyMMdd");

        //        var nowLifeSql = $@"SELECT
	       //                                 s.jz1lnfj1ljgzsj, 
	       //                                 s.jz1lnfj2ljgzsj, 
	       //                                 s.jz1lnfj1zcljgzsj, 
	       //                                 s.jz1lnfj2zcljgzsj, 
	       //                                 s.jz1tfj1ljgzsj, 
	       //                                 s.jz1tfj2ljgzsj, 
	       //                                 s.jz1tfj1zcljgzsj, 
	       //                                 s.jz1tfj2zcljgzsj, 
	       //                                 s.jz1ysj1ljgzsj, 
	       //                                 s.jz1ysj2ljgzsj, 
	       //                                 s.jz1kqjhqljgzsj, 
	       //                                 s.jz1zwxdljgzsj, 
	       //                                 s.jz1kqjhqdgljgzsj, 
	       //                                 s.jz1zwxddgljgzsj, 
	       //                                 s.jz1tfjjjtfjcqdzcs, 
	       //                                 s.jz1tfjjcqdzcs, 
	       //                                 s.jz1tfj2jcqdzcs, 
	       //                                 s.jz1lnfjjcqdzcs, 
	       //                                 s.jz1ysj1jcqdzcs, 
	       //                                 s.jz1ysj2jcqdzcs, 
	       //                                 s.lch, 
	       //                                 s.cxh, 
	       //                                 s.yxtzjid, 
	       //                                 s.device_code, 
	       //                                 s.cxhName, 
	       //                                 s.create_time
        //                                FROM
	       //                                 dbo.TB_PARSING_NEWDATAS AS s
        //                                ";
        //        var lifeData = await _db.SqlQueryable<TB_PARSING_NEWDATAS>(nowLifeSql).ToListAsync();

        //        var partsLifeData = await _db.Queryable<PartsLife>().Where(x => x.Type == "次").ToListAsync();

        //        var propertyCache = new ConcurrentDictionary<string, Func<object, object>>();

        //        foreach (var life in lifeData)
        //        {
        //            var data = partsLifeData.Where(x => x.CX == life.cxh && x.WZ == life.yxtzjid).ToList();

        //            if (data != null)
        //            {
        //                Type type = life.GetType();

        //                foreach (var item in data)
        //                {
        //                    string propertyName = item.Code;

        //                    // 尝试从缓存中获取属性访问器  
        //                    if (!propertyCache.TryGetValue(propertyName, out var getValue))
        //                    {
        //                        // 如果缓存中不存在，则使用反射创建并添加到缓存中  
        //                        PropertyInfo propertyInfo = type.GetProperty(propertyName);
        //                        if (propertyInfo != null)
        //                        {
        //                            // 注意：这里我们假设属性可以安全地转换为 object，然后在需要时转换为具体类型  
        //                            getValue = obj => propertyInfo.GetValue(obj, null);
        //                            propertyCache.TryAdd(propertyName, getValue); // 尝试添加到缓存中，防止并发时的重复添加  
        //                        }
        //                        else
        //                        {
        //                            // 如果属性不存在，则可以根据需要处理（例如记录错误、跳过等）  
        //                            continue; // 这里我们选择跳过当前 item  
        //                        }
        //                    }
        //                    var runLife = Convert.ToInt32(getValue(life));

        //                    item.RunLife = runLife;
        //                    item.updatetime = life.rq;

        //                    result.Add(item);

        //                }
        //            }
        //        }

        //        var updateNum = await _db.Updateable(result)
        //                 .UpdateColumns(it => new { it.RunLife, it.updatetime })
        //                 .WhereColumns(it => it.Id)
        //                 .ExecuteCommandAsync();
        //        await Console.Out.WriteLineAsync($"寿命数据更新成功,更新了{updateNum}条数据");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"更新寿命数据时出错：{ex.ToString()}");
        //    }
        //}

        ///// <summary>
        ///// 更新寿命数据并推送
        ///// </summary>
        ///// <param name="dataList"></param>
        ///// <returns></returns>
        ///// <exception cref="Exception"></exception>
        //private async Task AddOrUpdateSMDataV1()
        //{
        //    var result = new List<PartsLife>();

        //    try
        //    {
        //        var nowLifeSql = $@"SELECT
	       //                                 s.jz1lnfj1ljgzsj, 
	       //                                 s.jz1lnfj2ljgzsj, 
	       //                                 s.jz1lnfj1zcljgzsj, 
	       //                                 s.jz1lnfj2zcljgzsj, 
	       //                                 s.jz1tfj1ljgzsj, 
	       //                                 s.jz1tfj2ljgzsj, 
	       //                                 s.jz1tfj1zcljgzsj, 
	       //                                 s.jz1tfj2zcljgzsj, 
	       //                                 s.jz1ysj1ljgzsj, 
	       //                                 s.jz1ysj2ljgzsj, 
	       //                                 s.jz1kqjhqljgzsj, 
	       //                                 s.jz1zwxdljgzsj, 
	       //                                 s.jz1kqjhqdgljgzsj, 
	       //                                 s.jz1zwxddgljgzsj, 
	       //                                 s.jz1tfjjjtfjcqdzcs, 
	       //                                 s.jz1tfjjcqdzcs, 
	       //                                 s.jz1tfj2jcqdzcs, 
	       //                                 s.jz1lnfjjcqdzcs, 
	       //                                 s.jz1ysj1jcqdzcs, 
	       //                                 s.jz1ysj2jcqdzcs, 
	       //                                 s.lch, 
	       //                                 s.cxh, 
	       //                                 s.yxtzjid, 
	       //                                 s.device_code, 
	       //                                 s.cxhName, 
	       //                                 s.create_time,
        //                                    s.rq,
        //                                    s.jz1zwxdzt, 
	       //                                 s.jz1kqjhqzt, 
	       //                                 s.jz1tfj1zt, 
	       //                                 s.jz1tfj2zt, 
	       //                                 s.jz1lnfj1zt, 
	       //                                 s.jz1lnfj2zt, 
	       //                                 s.jz1ysj1zt, 
	       //                                 s.jz1ysjj2zt, 
	       //                                 s.jjtfzt
        //                                FROM
	       //                                 dbo.TB_PARSING_DATAS" + $"_{DateTime.Now:yyyyMMdd} " + " AS s";

        //        var time = DateTime.Now.AddHours(-1);
        //        var lifeData = await _db.SqlQueryable<TB_PARSING_DATAS>(nowLifeSql).Where(x => x.rq > time).ToListAsync();
        //        if (lifeData.Count == 0)
        //        {
        //            return;
        //        }

        //        var partsLifeData = await _db.Queryable<PartsLife>().Where(x => x.Type == "H").ToListAsync();

        //        var propertyCache = new ConcurrentDictionary<string, Func<object, object>>();

        //        var qTime = Convert.ToDecimal(_AddOrUpdateSMData);

        //        foreach (var life in partsLifeData)
        //        {
        //            var codeMappings = new Dictionary<string, (Func<TB_PARSING_DATAS, bool> condition, string zeroStatusProperty)>
        //            {
        //                { "jz1zwxdljgzsj", (x => x.jz1zwxdzt == 0, "jz1zwxdzt") },
        //                { "jz1zwxddgljgzsj", (x => x.jz1zwxdzt == 0, "jz1zwxdzt") },
        //                { "jz1kqjhqljgzsj", (x => x.jz1kqjhqzt == 0, "jz1kqjhqzt") },
        //                { "jz1kqjhqdgljgzsj", (x => x.jz1kqjhqzt == 0, "jz1kqjhqzt") },
        //                { "jz1lnfj1ljgzsj", (x => x.jz1lnfj1zt == 0, "jz1lnfj1zt") },
        //                { "jz1lnfj1zcljgzsj", (x => x.jz1lnfj1zt == 0, "jz1lnfj1zt") },
        //                { "jz1lnfj2ljgzsj", (x => x.jz1lnfj2zt == 0, "jz1lnfj2zt") },
        //                { "jz1lnfj2zcljgzsj", (x => x.jz1lnfj2zt == 0, "jz1lnfj2zt") },
        //                { "jz1tfj1ljgzsj", (x => x.jz1tfj1zt == 0, "jz1tfj1zt") },
        //                { "jz1tfj1zcljgzsj", (x => x.jz1tfj1zt == 0, "jz1tfj1zt") },
        //                { "jz1tfj2ljgzsj", (x => x.jz1tfj2zt == 0, "jz1tfj2zt") },
        //                { "jz1tfj2zcljgzsj", (x => x.jz1tfj2zt == 0, "jz1tfj2zt") },
        //                { "jz1ysj1ljgzsj", (x => x.jz1ysj1zt == 0, "jz1ysj1zt") },
        //                { "jz1ysj2ljgzsj", (x => x.jz1ysjj2zt == 0, "jz1ysjj2zt") }
        //            };

        //            if (codeMappings.TryGetValue(life.Code, out var mapping))
        //            {
        //                var runData = lifeData.Where(x => mapping.condition(x) && x.cxh == life.CX && x.yxtzjid == life.WZ);
        //                var runLife = Math.Round((decimal)runData.Count() / 3600, 1);
        //                life.RunLife += runLife;
        //                life.updatetime = runData.OrderByDescending(x => x.rq).FirstOrDefault().rq;
        //            }

        //            result.Add(life);
        //        }

        //        var updateNum = await _db.Updateable(result)
        //                 .UpdateColumns(it => new { it.RunLife, it.updatetime })
        //                 .WhereColumns(it => it.Id)
        //                 .ExecuteCommandAsync();
        //        await Console.Out.WriteLineAsync($"寿命数据更新成功,更新了{updateNum}条数据");

        //        var pushLifeData = result.Where(x => x.FaultCode != null).ToList();

        //        //寿命推送
        //        pushLifeData = pushLifeData.Where(x => !string.IsNullOrEmpty(x.FarecastCode)).ToList();
        //        await LifeDataPush(pushLifeData);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"更新寿命数据时出错：{ex.ToString()}");
        //    }
        //}

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

                //获取线路名称
                //var XL = config.Where(x => x.concode == _lineCode).First()?.conval;

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
        /// 温度异常预警模型
        /// </summary>
        /// <returns></returns>
        //private async Task GetWdFault()
        //{
        //    EquipmentFault xfWarn1, sfWarn1, sfWarn2, hfWarn1;
        //    var addFaults = new List<FaultOrWarn>();

        //    var config = _db.Queryable<SYS_CONFIG>().ToList();
        //    var nowData = _db.Queryable<TB_PARSING_NEWDATAS>().ToList();
        //    var equipments = await _db.Queryable<EquipmentFault>().ToListAsync();
        //    var faultData = _db.Queryable<FaultOrWarn>().ToList();
        //    //获取线路名称
        //    var XL = config.Where(x => x.concode == _lineCode).First().conval;

        //    var newData = await GetNewData("15");
        //    if (newData.Count == 0) return;

        //    foreach (var item in nowData)
        //    {
        //        if (item.yxtzjid == 1)
        //        {
        //            xfWarn1 = equipments.First(x => x.FaultCode == "hvac1024030001");

        //            sfWarn1 = equipments.First(x => x.FaultCode == "hvac1024030003");
        //            sfWarn2 = equipments.First(x => x.FaultCode == "hvac1024030004");

        //            hfWarn1 = equipments.First(x => x.FaultCode == "hvac1024030002");            
        //        }
        //        else
        //        {
        //            xfWarn1 = equipments.First(x => x.FaultCode == "hvac1024030005");

        //            sfWarn1 = equipments.First(x => x.FaultCode == "hvac1024030007");
        //            sfWarn2 = equipments.First(x => x.FaultCode == "hvac1024030008");

        //            hfWarn1 = equipments.First(x => x.FaultCode == "hvac1024030006");                 
        //        }


        //        var time10 = item.create_time.AddMinutes(-11);
        //        var time1 = item.create_time.AddMinutes(-1);

        //        var data10 = newData.Any(x => x.create_time >= time10 && x.tfms == 7);

        //        var data = newData.Where(x => x.device_code == item.device_code && x.create_time >= time1 && x.create_time < item.update_time);

        //        var xf1 = data.Any(x => x.jz1swwd > 60 || x.jz1swwd < 0);
        //        var xf1off = data.Any(x => x.jz1swwd > 70 || x.jz1swwd < -20);


        //        var sf1 = data.Any(x => x.jz1sfcgq1wd < -10 || x.jz1sfcgq1wd > 50);
        //        var sf2 = data.Any(x => x.jz1sfcgq2wd < -10 || x.jz1sfcgq2wd > 50);
        //        var hf1 = data.Any(x => x.jz1kswd < 0 || x.jz1kswdcgq1wd > 50 || x.jz1kswdcgq1wd < 0 || x.jz1kswdcgq1wd > 50);        

        //        var xf1F = faultData.Any(x => x.Code == xfWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);         
        //        var sf1F = faultData.Any(x => x.Code == sfWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);
        //        var sf2F = faultData.Any(x => x.Code == sfWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);
        //        var hf1F = faultData.Any(x => x.Code == hfWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);


        //        var addfault = new FaultOrWarn
        //        {
        //            xlh = XL,
        //            lch = item.lch,
        //            cxh = item.cxh,
        //            DeviceCode = item.device_code,
        //            Type = "2",
        //            State = "1",
        //            createtime = item.update_time
        //        };

        //        if (!data10 && xf1 && !xf1F)
        //        {
        //            addfault.Code = xfWarn1.FaultCode;
        //            addfault.Name = xfWarn1.FaultName;
        //            addfault.FaultType = xfWarn1.Type;
        //            addFaults.Add(addfault);
        //        }

        //        if (!data10 && sf1 && !sf2F)
        //        {
        //            addfault.Code = sfWarn1.FaultCode;
        //            addfault.Name = sfWarn1.FaultName;
        //            addfault.FaultType = sfWarn1.Type;
        //            addFaults.Add(addfault);
        //        }

        //        if (!data10 && sf2 && !sf2F)
        //        {
        //            addfault.Code = sfWarn2.FaultCode;
        //            addfault.Name = sfWarn2.FaultName;
        //            addfault.FaultType = sfWarn2.Type;
        //            addFaults.Add(addfault);
        //        }

        //        if (!data10 && hf1 && !hf1F)
        //        {
        //            addfault.Code = hfWarn1.FaultCode;
        //            addfault.Name = hfWarn1.FaultName;
        //            addfault.FaultType = hfWarn1.Type;
        //            addFaults.Add(addfault);
        //        }
        //    }

        //    if (addFaults.Count > 0)
        //    {
        //        var faultReq = await FaultSetHttpPost(addFaults, new List<FaultOrWarn>());

        //        if (faultReq != null && faultReq.result_code == "200")
        //        {
        //            _logger.LogInformation($"温度异常预警推送成功，新增了{addFaults.Count}条预警");

        //            for (int i = 0; i < addFaults.Count; i++)
        //            {
        //                addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
        //            }
        //        }
        //        var addnum = _db.Insertable(addFaults).ExecuteCommand();
        //        _logger.LogInformation($"温度异常预警同步完成，新增了{addnum}条预警");
        //    }
        //}

        /// <summary>
        /// 压缩机异常预警模型
        /// </summary>
        /// <returns></returns>
        private async Task GetYsjFault()
        {
            OVERHAULIDEA pqWarn1, pqWarn2, xqWarn1, xqWarn2, gyWarn1, gyWarn2, dyWarn1, dyWarn2;
            var addFaults = new List<FAULTWARN>();

            var config = _db.Queryable<SYS_CONFIG>().ToList();
            var nowData = _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToList();
            var equipments = await _db.Queryable<OVERHAULIDEA>().ToListAsync();
            var faultData = _db.Queryable<FAULTWARN>().ToList();

            string sql = @"SELECT * FROM
							    dbo.TB_PARSING_DATAS_YJ_2" + $"_{DateTime.Now:yyyyMMdd} " +
                         @"WHERE
								create_time >= DATEADD(MINUTE,-30,GETDATE())";

            var newDatas = await _db.SqlQueryable<TB_PARSING_DATAS_YJ_2>(sql).ToListAsync();
            if (newDatas.Count == 0) return;

            foreach (var item in nowData)
            {

                //pqWarn1 = equipments.First(x => x.FaultCode == "hvac1014150001");
                //pqWarn2 = equipments.First(x => x.FaultCode == "hvac1014150002");

                //xqWarn1 = equipments.First(x => x.FaultCode == "hvac1014160001");
                //xqWarn2 = equipments.First(x => x.FaultCode == "hvac1014160002");

                11Warn1 = equipments.First(x => x.faultcode == "1x07036");
                12Warn2 = equipments.First(x => x.faultcode == "1x07037");

                21Warn1 = equipments.First(x => x.faultcode == "1x07038");
                22Warn2 = equipments.First(x => x.faultcode == "1x07039");

                //pqWarn1 = equipments.First(x => x.FaultCode == "hvac1014150003");
                //pqWarn2 = equipments.First(x => x.FaultCode == "hvac1014150004");

                //xqWarn1 = equipments.First(x => x.FaultCode == "hvac1014160003");
                //xqWarn2 = equipments.First(x => x.FaultCode == "hvac1014160004");

                //gyWarn1 = equipments.First(x => x.faultcode == "hvac1014020006");
                //gyWarn2 = equipments.First(x => x.faultcode == "hvac1014020008");

                //dyWarn1 = equipments.First(x => x.faultcode == "hvac1014020002");
                //dyWarn2 = equipments.First(x => x.faultcode == "hvac1014020004");


                var time10 = item.create_time.AddMinutes(-11);
                var time1 = item.create_time.AddMinutes(-1);

                var ysj1data10 = newData.Any(x => x.create_time >= time10 && x.jz1ysj1zt == 0);
                var ysj2data10 = newData.Any(x => x.create_time >= time10 && x.jz1ysjj2zt == 0);

                var data = newData.Where(x => x.device_code == item.device_code && x.create_time >= time1 && x.create_time < item.update_time);

                var pq1 = data.Any(x => x.jz1ysj1pqwd > 125 || x.jz1ysj1pqwd < 30);
                var pq2 = data.Any(x => x.jz1ysj2pqwd > 125 || x.jz1ysj2pqwd < 30);

                var xq1 = data.Any(x => x.jz1ysj1xqwd < -10 || x.jz1ysj1xqwd > 40);
                var xq2 = data.Any(x => x.jz1ysj2xqwd < -10 || x.jz1ysj2xqwd > 40);

                var gy1 = data.Any(x => x.jz1ysj1gyyl < 1000 || x.jz1ysj1gyyl > 3000);
                var gy2 = data.Any(x => x.jz1ysj2gyyl < 1000 || x.jz1ysj2gyyl > 3000);

                var dy1 = data.Any(x => x.jz1ysj1dyyl < 300 || x.jz1ysj1dyyl > 800);
                var dy2 = data.Any(x => x.jz1ysj2dyyl < 300 || x.jz1ysj2dyyl > 800);

                var pq1F = faultData.Any(x => x.Code == pqWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);
                var pq2F = faultData.Any(x => x.Code == pqWarn2.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);

                var xq1F = faultData.Any(x => x.Code == xqWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);
                var xq2F = faultData.Any(x => x.Code == xqWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);

                var gy1F = faultData.Any(x => x.Code == gyWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);
                var gy2F = faultData.Any(x => x.Code == gyWarn2.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);

                var dy1F = faultData.Any(x => x.Code == dyWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);
                var dy2F = faultData.Any(x => x.Code == dyWarn2.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);


                var addfault = new FaultOrWarn
                {
                    xlh = XL,
                    lch = item.lch,
                    cxh = item.cxh,
                    DeviceCode = item.device_code,
                    Type = "2",
                    State = "1",
                    createtime = item.update_time
                };

                if (!ysj1data10 && pq1 && !pq1F)
                {
                    addfault.Code = pqWarn1.FaultCode;
                    addfault.Name = pqWarn1.FaultName;
                    addfault.FaultType = pqWarn1.Type;
                    addFaults.Add(addfault);
                }

                if (!ysj2data10 && pq2 && !pq2F)
                {
                    addfault.Code = pqWarn2.FaultCode;
                    addfault.Name = pqWarn2.FaultName;
                    addfault.FaultType = pqWarn2.Type;
                    addFaults.Add(addfault);
                }

                if (!ysj1data10 && xq1 && !xq1F)
                {
                    addfault.Code = xqWarn1.FaultCode;
                    addfault.Name = xqWarn1.FaultName;
                    addfault.FaultType = xqWarn1.Type;
                    addFaults.Add(addfault);
                }

                if (!ysj2data10 && xq2 && !xq2F)
                {
                    addfault.Code = xqWarn2.FaultCode;
                    addfault.Name = xqWarn2.FaultName;
                    addfault.FaultType = xqWarn2.Type;
                    addFaults.Add(addfault);
                }

                if (!ysj1data10 && gy1 && !gy1F)
                {
                    addfault.Code = gyWarn1.FaultCode;
                    addfault.Name = gyWarn1.FaultName;
                    addfault.FaultType = gyWarn1.Type;
                    addFaults.Add(addfault);
                }

                if (!ysj2data10 && gy2 && !gy2F)
                {
                    addfault.Code = gyWarn2.FaultCode;
                    addfault.Name = gyWarn2.FaultName;
                    addfault.FaultType = gyWarn2.Type;
                    addFaults.Add(addfault);
                }

                if (!ysj1data10 && dy1 && !dy1F)
                {
                    addfault.Code = dyWarn1.FaultCode;
                    addfault.Name = dyWarn1.FaultName;
                    addfault.FaultType = dyWarn1.Type;
                    addFaults.Add(addfault);
                }

                if (!ysj2data10 && dy2 && !dy2F)
                {
                    addfault.Code = dyWarn2.FaultCode;
                    addfault.Name = dyWarn2.FaultName;
                    addfault.FaultType = dyWarn2.Type;
                    addFaults.Add(addfault);
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

        ///// <summary>
        ///// 设备运行电流异常预警模型
        ///// </summary>
        ///// <returns></returns>
        //private async Task GetSbdlzFault()
        //{
        //    EquipmentFault Warn1, Warn2, LnWarn1, LnWarn2, ysjWarn1, ysjWarn2;
        //    var addFaults = new List<FaultOrWarn>();

        //    var config = _db.Queryable<SYS_CONFIG>().ToList();
        //    var nowData = _db.Queryable<TB_PARSING_NEWDATAS>().ToList();
        //    var equipments = await _db.Queryable<EquipmentFault>().ToListAsync();
        //    var faultData = _db.Queryable<FaultOrWarn>().ToList();
        //    //获取线路名称
        //    var XL = config.Where(x => x.concode == _lineCode).First().conval;

        //    var newData = await GetNewData(null);
        //    if (newData.Count == 0) return;

        //    foreach (var item in nowData)
        //    {
        //        if (item.yxtzjid == 1)
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014090001");
        //            Warn2 = equipments.First(x => x.FaultCode == "hvac1014090002");

        //            LnWarn1 = equipments.First(x => x.FaultCode == "hvac1014100001");
        //            LnWarn2 = equipments.First(x => x.FaultCode == "hvac1014100002");

        //            ysjWarn1 = equipments.First(x => x.FaultCode == "hvac1014110001");
        //            ysjWarn2 = equipments.First(x => x.FaultCode == "hvac1014110002");
        //        }
        //        else
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014090003");
        //            Warn2 = equipments.First(x => x.FaultCode == "hvac1014090004");

        //            LnWarn1 = equipments.First(x => x.FaultCode == "hvac1014100003");
        //            LnWarn2 = equipments.First(x => x.FaultCode == "hvac1014100004");

        //            ysjWarn1 = equipments.First(x => x.FaultCode == "hvac1014110003");
        //            ysjWarn2 = equipments.First(x => x.FaultCode == "hvac1014110004");
        //        }

        //        var data = newData.Where(x => x.device_code == item.device_code);

        //        var tfj1 = data.Any(x => x.jz1tfj1uxdlz < 1 || x.jz1tfj1vxdlz < 1 || x.jz1tfj1wxdlz < 1);
        //        var tfj2 = data.Any(x => x.jz1tfj2uxdlz < 1 || x.jz1tfj2vxdlz < 1 || x.jz1tfj2wxdlz < 1);

        //        var lnfj1 = data.Any(x => x.jz1lnfj1uxdlz < 1.2 || x.jz1lnfj1vxdlz < 1.2 || x.jz1lnfj1wxdlz < 1.2);
        //        var lnfj2 = data.Any(x => x.jz1lnfj2uxdlz < 1.2 || x.jz1lnfj2vxdlz < 1.2 || x.jz1lnfj2wxdlz < 1.2);

        //        var ysj1 = data.Any(x => x.jz1ysj1uxdlz < 4 || x.jz1ysj1vxdlz < 4 || x.jz1ysj1wxdlz < 4 || x.jz1ysj1uxdlz > 25 || x.jz1ysj1vxdlz > 25 || x.jz1ysj1wxdlz > 25);
        //        var ysj2 = data.Any(x => x.jz1ysj2uxdlz < 4 || x.jz1ysj2vxdlz < 4 || x.jz1ysj2wxdlz < 4 || x.jz1ysj2uxdlz > 25 || x.jz1ysj2vxdlz > 25 || x.jz1ysj2wxdlz > 25);

        //        var tfj1F = faultData.Any(x => x.Code == Warn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);
        //        var tfj2F = faultData.Any(x => x.Code == Warn2.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);

        //        var lnfj1F = faultData.Any(x => x.Code == LnWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);
        //        var lnfj2F = faultData.Any(x => x.Code == LnWarn2.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);

        //        var ysj1F = faultData.Any(x => x.Code == ysjWarn1.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);
        //        var ysj2F = faultData.Any(x => x.Code == ysjWarn2.FaultCode && x.State == "1" && x.DeviceCode == item.device_code);

        //        var addfault = new FaultOrWarn
        //        {
        //            xlh = XL,
        //            lch = item.lch,
        //            cxh = item.cxh,
        //            DeviceCode = item.device_code,
        //            Type = "2",
        //            State = "1",
        //            createtime = item.update_time
        //        };

        //        if (item.jz1tfj1zt == 1 && !tfj1F && tfj1)
        //        {
        //            addfault.Code = Warn1.FaultCode;
        //            addfault.Name = Warn1.FaultName;
        //            addfault.FaultType = Warn1.Type;                      
        //            addFaults.Add(addfault);
        //        }

        //        if (item.jz1tfj2zt == 1 && !tfj2F && tfj2)
        //        {
        //            addfault.Code = Warn2.FaultCode;
        //            addfault.Name = Warn2.FaultName;
        //            addfault.FaultType = Warn2.Type;
        //            addFaults.Add(addfault);
        //        }

        //        if (item.jz1lnfj1zt == 1 && !lnfj1F && lnfj1)
        //        {
        //            addfault.Code = LnWarn1.FaultCode;
        //            addfault.Name = LnWarn1.FaultName;
        //            addfault.FaultType = LnWarn1.Type;
        //            addFaults.Add(addfault);
        //        }

        //        if (item.jz1lnfj2zt == 1 && !lnfj2F && lnfj2)
        //        {
        //            addfault.Code = LnWarn2.FaultCode;
        //            addfault.Name = LnWarn2.FaultName;
        //            addfault.FaultType = LnWarn2.Type;
        //            addFaults.Add(addfault);
        //        }

        //        if (item.jz1ysj1zt == 1 && !ysj1F && ysj1)
        //        {
        //            addfault.Code = ysjWarn1.FaultCode;
        //            addfault.Name = ysjWarn1.FaultName;
        //            addfault.FaultType = ysjWarn1.Type;
        //            addFaults.Add(addfault);
        //        }

        //        if (item.jz1ysjj2zt == 1 && !ysj2F && ysj2)
        //        {
        //            addfault.Code = ysjWarn2.FaultCode;
        //            addfault.Name = ysjWarn2.FaultName;
        //            addfault.FaultType = ysjWarn2.Type;
        //            addFaults.Add(addfault);
        //        }
        //    }

        //    if (addFaults.Count > 0)
        //    {
        //        var faultReq = await FaultSetHttpPost(addFaults, new List<FaultOrWarn>());

        //        if (faultReq != null && faultReq.result_code == "200")
        //        {
        //            _logger.LogInformation($"设备运行电流异常预警推送成功，新增了{addFaults.Count}条预警");

        //            for (int i = 0; i < addFaults.Count; i++)
        //            {
        //                addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
        //            }
        //        }
        //        var addnum = _db.Insertable(addFaults).ExecuteCommand();
        //        _logger.LogInformation($"设备运行电流异常预警同步完成，新增了{addnum}条预警");
        //    }
        //}

        //#region 寿命预警模型

        /////// <summary>
        /////// 寿命异常预警模型
        /////// </summary>
        /////// <param name="data"></param>
        /////// <param name="equipments"></param>
        /////// <param name="XL"></param>
        /////// <returns></returns>
        //private (FaultOrWarn?, FaultOrWarn?) GetLifeFault(PartsLife data, List<EquipmentFault> equipments, string XL)
        //{
        //    EquipmentFault equipment;
        //    FaultOrWarn? faultOrWarn = null, upWarn = null, isAny1;

        //    string device_code = data.CX + "_" + data.WZ;

        //    equipment = equipments.First(x => x.FaultCode == data.FaultCode);

        //    var faultOrWarns = _db.Queryable<FaultOrWarn>()
        //                        .Where(x => x.DeviceCode == device_code && x.Code == equipment.FaultCode)
        //                        .ToList();

        //    isAny1 = faultOrWarns.FirstOrDefault(x => x.Code == equipment.FaultCode);

        //    //addAny1 = faults.FirstOrDefault(x => x.DeviceCode == device_code && x.Code == equipment.FaultCode);
        //    //addAny2 = faults.FirstOrDefault(x => x.DeviceCode == device_code && x.Code == Warn2.FaultCode);

        //    if (data.RunLife > data.RatedLife)
        //    {
        //        if (isAny1 == null)
        //        {
        //            //创建 faultOrWarn 对象并设置属性
        //            faultOrWarn = new FaultOrWarn
        //            {
        //                xlh = XL,
        //                lch = data.CH,
        //                cxh = data.CX,
        //                DeviceCode = device_code,
        //                Code = equipment.FaultCode,
        //                Name = equipment.FaultName,
        //                FaultType = equipment.Type,
        //                Type = "2",
        //                State = "1",
        //                createtime = data.updatetime
        //            };
        //        }
        //    }
        //    else
        //    {
        //        if (isAny1 != null && isAny1.State == "1")
        //        {
        //            isAny1.State = "0";
        //            isAny1.updatetime = data.updatetime;
        //            upWarn = isAny1;
        //        }
        //    }

        //    return (faultOrWarn, upWarn);
        //}

        ///// <summary>
        ///// 车内空气质量预警模型
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="equipments"></param>
        ///// <param name="XL"></param>
        ///// <returns></returns>
        //private async Task GetKqzlFault()
        //{
        //    EquipmentFault Warn1;
        //    var addFaults = new List<FaultOrWarn>();

        //    var config = _db.Queryable<SYS_CONFIG>().ToList();
        //    var nowData = _db.Queryable<TB_PARSING_NEWDATAS>().ToList();
        //    var equipments = await _db.Queryable<EquipmentFault>().ToListAsync();
        //    //获取线路名称
        //    var XL = config.Where(x => x.concode == _lineCode).First().conval;

        //    var newData = await GetNewData("15");
        //    if (newData.Count == 0) return;

        //    Warn1 = equipments.First(x => x.FaultCode == "hvac1014130001");

        //    var groupData = nowData.GroupBy(x => x.cxh);

        //    var faultOrWarns = _db.Queryable<FaultOrWarn>().Where(x => x.Code == Warn1.FaultCode && x.State == "1");
        //    foreach (var item in groupData)
        //    {
        //        var isFault = faultOrWarns.Any(x => x.cxh == item.Key);

        //        if (isFault) continue;

        //        var dataList = newData.Where(x => x.cxh == item.Key).ToList();

        //        var isTrue1 = dataList.Any(x => x.kssdz < 80);
        //        var isTrue2 = dataList.Any(x => x.jz1co2nd <= 1500);
        //        var isTrue3 = dataList.Any(x => (x.jz1kswd + x.jz1kswdcgq1wd + x.jz1kqzljcmkwd) / 3 >= 20 && (x.jz1kswd + x.jz1kswdcgq1wd + x.jz1kqzljcmkwd) / 3 <= 28);

        //        if (isTrue1 || isTrue2 || isTrue3)
        //        {
        //            var addFault = new FaultOrWarn
        //            {
        //                xlh = XL,
        //                lch = item.First().lch,
        //                cxh = item.Key,
        //                DeviceCode = item.First().device_code,
        //                Code = Warn1.FaultCode,
        //                Name = Warn1.FaultName,
        //                FaultType = Warn1.Type,
        //                Type = "2",
        //                State = "1",
        //                createtime = item.First().create_time
        //            };
        //            addFaults.Add(addFault);
        //        }
        //    }

        //    if (addFaults.Count > 0)
        //    {
        //        var faultReq = await FaultSetHttpPost(addFaults, new List<FaultOrWarn>());

        //        if (faultReq != null && faultReq.result_code == "200")
        //        {
        //            _logger.LogInformation($"车内空气质量预警推送成功，新增了{addFaults.Count}条预警");

        //            for (int i = 0; i < addFaults.Count; i++)
        //            {
        //                addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
        //            }
        //        }
        //        var addnum = _db.Insertable(addFaults).ExecuteCommand();
        //        _logger.LogInformation($"车内空气质量预警同步完成，新增了{addnum}条预警");
        //    }

        //}


        /// <summary>
        /// 制冷目标温度异常预警模型
        /// </summary>
        /// <returns></returns>
        private async Task GetZlmbwdFault()
        {
            try
            {
                var addFaults = new List<FAULTWARN>();
                //var config = _db.Queryable<SYS_CONFIG>().ToList();
                var nowData = await _db.Queryable<TB_PARSING_DATAS_NEWCS>().ToListAsync();
                var equipments = await _db.Queryable<OVERHAULIDEA>().ToListAsync();
                var lchs = _db.Queryable<LCH>().ToList();
                var cxhs = _db.Queryable<CXH>().ToList();

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
                        var lcid = lchs.FirstOrDefault(x => x.lch == item.lch);
                        var cxid = cxhs.FirstOrDefault(x => x.lcid == lcid.id && x.cxh == item.cxh);
                        var addFault = new FAULTWARN
                        {
                            line_id = Convert.ToInt32(lcid.xl_id),
                            lch = item.lch,
                            lcid = lcid.id,
                            cxid = cxid.id,
                            cxh = item.cxh,
                            sbid = (int?)item.id,
                            sbbm = item.device_code,
                            xdid = Warn1.id,
                            type = Warn1.type,
                            xdmc = Warn1.jxname,
                            state = "0",
                            gzjb = Warn1.gzdj,
                            collect_time = DateTime.Now,
                            createtime = DateTime.Now
                        };
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

        ///// <summary>
        ///// 制冷剂泄露预警模型
        ///// </summary>
        ///// <returns></returns>
        //private async Task GetZljxlFault()
        //{
        //    try
        //    {
        //        EquipmentFault Warn1, Warn2;
        //        var addFaults = new List<FaultOrWarn>();

        //        var config = _db.Queryable<SYS_CONFIG>().ToList();
        //        var nowData = _db.Queryable<TB_PARSING_NEWDATAS>().ToList();
        //        var equipments = await _db.Queryable<EquipmentFault>().ToListAsync();
        //        //获取线路名称
        //        var XL = config.Where(x => x.concode == _lineCode).First().conval;

        //        var newData = await GetNewData(null);

        //        if (newData.Count == 0) return;

        //        foreach (var item in nowData)
        //        {

        //            if (item.yxtzjid == 1)
        //            {
        //                Warn1 = equipments.First(x => x.FaultCode == "hvac1014050001");
        //                Warn2 = equipments.First(x => x.FaultCode == "hvac1014050002");
        //            }
        //            else
        //            {
        //                Warn1 = equipments.First(x => x.FaultCode == "hvac1014050003");
        //                Warn2 = equipments.First(x => x.FaultCode == "hvac1014050004");
        //            }

        //            var faultOrWarns = _db.Queryable<FaultOrWarn>()
        //                            .Where(x => x.DeviceCode == item.device_code && (x.Code == Warn1.FaultCode || x.Code == Warn2.FaultCode))
        //                            .ToList();

        //            var isAny1 = faultOrWarns.FirstOrDefault(x => x.Code == Warn1.FaultCode);
        //            var isAny2 = faultOrWarns.FirstOrDefault(x => x.Code == Warn2.FaultCode);

        //            var ysj1data = newData.Where(x => x.device_code == item.device_code && x.jz1ysj1zt == 0);
        //            var ysj2data = newData.Where(x => x.device_code == item.device_code && x.jz1ysjj2zt == 0);

        //            var isTrue1 = ysj1data.Any(x => x.jz1ysj1gyyl < 600 || x.jz1ysj1dyyl < 600);
        //            var isTrue2 = ysj1data.Any(x => x.jz1ysj2gyyl < 600 || x.jz1ysj2dyyl < 600);

        //            if (isAny1 == null && isTrue1)
        //            {
        //                var addFault = new FaultOrWarn
        //                {
        //                    xlh = XL,
        //                    lch = item.lch,
        //                    cxh = item.cxh,
        //                    DeviceCode = item.device_code,
        //                    Code = Warn1.FaultCode,
        //                    Name = Warn1.FaultName,
        //                    FaultType = Warn1.Type,
        //                    Type = "2",
        //                    State = "1",
        //                    createtime = item.create_time
        //                };
        //                addFaults.Add(addFault);
        //            }
        //            if (isAny2 == null && isTrue2)
        //            {
        //                var addFault = new FaultOrWarn
        //                {
        //                    xlh = XL,
        //                    lch = item.lch,
        //                    cxh = item.cxh,
        //                    DeviceCode = item.device_code,
        //                    Code = Warn2.FaultCode,
        //                    Name = Warn2.FaultName,
        //                    FaultType = Warn2.Type,
        //                    Type = "2",
        //                    State = "1",
        //                    createtime = item.create_time
        //                };
        //                addFaults.Add(addFault);
        //            }
        //        }

        //        var faultReq = await FaultSetHttpPost(addFaults, new List<FaultOrWarn>());

        //        if (faultReq != null && faultReq.result_code == "200")
        //        {
        //            _logger.LogInformation($"制冷剂泄露预警推送成功，新增了{addFaults.Count}条预警");

        //            for (int i = 0; i < addFaults.Count; i++)
        //            {
        //                addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
        //            }
        //        }

        //        if (addFaults.Count > 0)
        //        {
        //            var addnum = _db.Insertable(addFaults).ExecuteCommand();
        //            _logger.LogInformation($"制冷剂泄露预警同步完成，新增了{addnum}条预警");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"制冷剂泄露预警失败：{ex.ToString()}");
        //    }
        //}

        ///// <summary>
        ///// 滤网脏堵预警模型
        ///// </summary>
        ///// <returns></returns>
        //private async Task GetLwzdFault()
        //{
        //    EquipmentFault Warn1;
        //    var addFaults = new List<FaultOrWarn>();
        //    var setFaults = new List<FaultOrWarn>();

        //    var config = _db.Queryable<SYS_CONFIG>().ToList();
        //    var nowData = _db.Queryable<TB_PARSING_NEWDATAS>().Where(x => x.tfms == 0).ToList();
        //    var equipments = await _db.Queryable<EquipmentFault>().ToListAsync();
        //    var faultDataQue = _db.Queryable<FaultOrWarn>().Where(x => x.State == "1");
        //    //获取线路名称
        //    var XL = config.Where(x => x.concode == _lineCode).First().conval;

        //    var newData = await GetNewData(null);

        //    if (newData.Count == 0) return;  

        //    foreach (var item in nowData)
        //    {
        //        if (item.yxtzjid == 1)
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014080001");
        //        }
        //        else
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014080002");
        //        }

        //        var faultData = faultDataQue.Where(x => x.DeviceCode == item.device_code && x.Code == Warn1.FaultCode);

        //        var isAny1 = faultData.Any(x => x.Type == "2");

        //        var startOfDay = new DateTime(item.update_time.Year, item.update_time.Month, item.update_time.Day, 0, 0, 0);
        //        var isAny2 = faultData.Any(x => x.Type == "4" && x.createtime > startOfDay);

        //        if (isAny1 || isAny2) continue;

        //        var time = item.create_time.AddMinutes(-3);
        //        var time1 = item.create_time.AddMinutes(-1);
        //        var dataList = newData.Where(x => x.device_code == item.device_code && x.create_time >= time);

        //        var isTrue1 = !dataList.Any(x => x.jz1tfj1zt == 0);
        //        var isTrue2 = !dataList.Any(x => x.jz1tfj2zt == 0);

        //        var data1minute = dataList.Where(x => x.create_time >= time1);
        //        var isFault1 = data1minute.Any(x => x.jz1lwylz >= 100);
        //        var isFault2 = data1minute.Any(x => x.jz1lwylz >= 150);

        //        if (isFault1)
        //        {
        //            var addFault = new FaultOrWarn
        //            {
        //                xlh = XL,
        //                lch = item.lch,
        //                cxh = item.cxh,
        //                DeviceCode = item.device_code,
        //                Code = Warn1.FaultCode,
        //                Name = Warn1.FaultName,
        //                FaultType = Warn1.Type,
        //                Type = "4",
        //                State = "1",
        //                createtime = item.create_time
        //            };
        //            addFaults.Add(addFault);

        //            var count = faultData.Where(x => x.Type == "4").Count();

        //            if (count >= 9)
        //            {
        //                addFault.Type = "2";
        //                addFaults.Add(addFault);
        //                setFaults.Add(addFault);
        //            }
        //        }

        //        if (isFault2)
        //        {
        //            var addFault = new FaultOrWarn
        //            {
        //                xlh = XL,
        //                lch = item.lch,
        //                cxh = item.cxh,
        //                DeviceCode = item.device_code,
        //                Code = Warn1.FaultCode,
        //                Name = Warn1.FaultName,
        //                FaultType = Warn1.Type,
        //                Type = "5",
        //                State = "1",
        //                createtime = item.create_time
        //            };
        //            addFaults.Add(addFault);

        //            var count = faultData.Where(x => x.Type == "5").Count();

        //            if (count >= 29)
        //            {
        //                addFault.Type = "3";
        //                addFault.Name = Warn1.FaultName[..^2] + "报警";
        //                addFaults.Add(addFault);
        //                setFaults.Add(addFault);
        //            }
        //        }                             
        //    }

        //    var faultReq = await FaultSetHttpPost(setFaults, new List<FaultOrWarn>());

        //    if (faultReq != null && faultReq.result_code == "200")
        //    {
        //        _logger.LogInformation($"冷凝进出风短路预警推送成功，新增了{setFaults.Count}条预警");

        //        for (int i = 0; i < addFaults.Count; i++)
        //        {
        //            addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
        //        }
        //    }

        //    if (addFaults.Count > 0)
        //    {
        //        var addnum = _db.Insertable(addFaults).ExecuteCommand();
        //        _logger.LogInformation($"冷凝进出风短路预警同步完成，新增了{addnum}条预警");
        //    }
        //}


        ///// <summary>
        ///// 蒸发器脏堵预警模型
        ///// </summary>
        ///// <returns></returns>
        //private async Task GetZfqzdFault()
        //{
        //    EquipmentFault Warn1;
        //    var addFaults = new List<FaultOrWarn>();
        //    var setFaults = new List<FaultOrWarn>();

        //    var config = _db.Queryable<SYS_CONFIG>().ToList();
        //    var nowData = _db.Queryable<TB_PARSING_NEWDATAS>().ToList();
        //    var equipments = await _db.Queryable<EquipmentFault>().ToListAsync();
        //    //获取线路名称
        //    var XL = config.Where(x => x.concode == _lineCode).First().conval;

        //    var newData = await GetNewData(null);

        //    if (newData.Count == 0) return;

        //    foreach (var item in nowData)
        //    {
        //        var isFault = item.tfms == 0 && item.jz1xff1kd == 100 && item.jz1hff1kd == 100 && item.jz1xff2kd == 100 && item.jz1hff2kd == 100 && item.jz1lwylz <= 40;
        //        if (!isFault) continue;

        //        if (item.yxtzjid == 1)
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014170001");
        //        }
        //        else
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014170002");
        //        }

        //        var dataList = newData.Where(x => x.device_code == item.device_code).OrderBy(x => x.create_time).ToList();

        //        var startOfDay = new DateTime(item.update_time.Year, item.update_time.Month, item.update_time.Day, 0, 0, 0);

        //        var faultData = _db.Queryable<FaultOrWarn>().Where(x => x.DeviceCode == item.device_code && x.Code == Warn1.FaultCode && x.State == "1");

        //        var isAny1 = faultData.Any(x => x.Type == "2");
        //        var isAny2 = faultData.Any(x => x.Type == "4" && x.createtime > startOfDay);

        //        if (isAny1 || isAny2) continue;

        //        var time = item.update_time.AddMinutes(-3);
        //        var dataListTimeQ = dataList.Where(x => x.create_time >= time && x.create_time < item.update_time);

        //        var time1 = item.update_time.AddMinutes(-1);
        //        var minutes1Data = dataList.Where(x => x.create_time >= time1 && x.create_time < item.update_time).OrderBy(x => x.create_time);

        //        var AP1 = minutes1Data.Average(x => (x.jz1tfj1uxdlz + x.jz1tfj1vxdlz + x.jz1tfj1wxdlz) / 3);
        //        var AP2 = minutes1Data.Average(x => (x.jz1tfj2uxdlz + x.jz1tfj2vxdlz + x.jz1tfj2wxdlz) / 3);


        //        var tfj1 = AP1 >= 0.9 * 1.1 && AP1 <= 1.1;
        //        var tfj2 = AP2 >= 0.9 * 1.1 && AP2 <= 1.1;

        //        var isTrue1 = !dataListTimeQ.Any(x => x.jz1tfj1zt == 0) && tfj1;
        //        var isTrue2 = !dataListTimeQ.Any(x => x.jz1tfj2zt == 0) && tfj2;

        //        if (isTrue1 || isTrue2)
        //        {
        //            var addFault = new FaultOrWarn
        //            {
        //                xlh = XL,
        //                lch = item.lch,
        //                cxh = item.cxh,
        //                DeviceCode = item.device_code,
        //                Code = Warn1.FaultCode,
        //                Name = Warn1.FaultName,
        //                FaultType = Warn1.Type,
        //                Type = "3",
        //                State = "1",
        //                createtime = item.create_time
        //            };
        //            addFaults.Add(addFault);

        //            var count = faultData.Where(x => x.Type == "3").Count();

        //            if (count >= 14)
        //            {
        //                addFault.Type = "2";
        //                addFaults.Add(addFault);
        //                setFaults.Add(addFault);
        //            }
        //        }            
        //    }

        //    var faultReq = await FaultSetHttpPost(setFaults, new List<FaultOrWarn>());

        //    if (faultReq != null && faultReq.result_code == "200")
        //    {
        //        _logger.LogInformation($"冷凝进出风短路预警推送成功，新增了{setFaults.Count}条预警");

        //        for (int i = 0; i < addFaults.Count; i++)
        //        {
        //            addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
        //        }
        //    }

        //    if (addFaults.Count > 0)
        //    {
        //        var addnum = _db.Insertable(addFaults).ExecuteCommand();


        //        _logger.LogInformation($"冷凝进出风短路预警同步完成，新增了{addnum}条预警");
        //    }
        //}

        ///// <summary>
        ///// 冷凝器脏堵预警模型
        ///// </summary>
        ///// <returns></returns>
        //private async Task GetLnqzdFault()
        //{
        //    EquipmentFault Warn1;
        //    var addFaults = new List<FaultOrWarn>();
        //    var setFaults = new List<FaultOrWarn>();

        //    var config = _db.Queryable<SYS_CONFIG>().ToList();
        //    var nowData = _db.Queryable<TB_PARSING_NEWDATAS>().ToList();
        //    var equipments = await _db.Queryable<EquipmentFault>().ToListAsync();
        //    var faultOrWarn = _db.Queryable<FaultOrWarn>().ToList();
        //    //获取线路名称
        //    var XL = config.Where(x => x.concode == _lineCode).First().conval;

        //    var newData = await GetNewData(null);
        //    if (newData.Count == 0) return;

        //    foreach (var item in nowData)
        //    {

        //        if (!new[] { 2, 3, 4, 5 }.Contains(item.tfms))
        //        {
        //            continue;
        //        }

        //        if (item.yxtzjid == 1)
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014180001");
        //        }
        //        else
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014180002");
        //        }

        //        var faultData = faultOrWarn.Where(x => x.DeviceCode == item.device_code && x.Code == Warn1.FaultCode && x.State == "1");

        //        var startOfDay = new DateTime(item.update_time.Year, item.update_time.Month, item.update_time.Day, 0, 0, 0);

        //        var dataList = newData.Where(x => x.device_code == item.device_code).ToList();
        //        var isAny1 = faultData.Any(x => x.Type == "2");
        //        var isAny2 = faultData.Any(x => x.Type == "4" && x.createtime > startOfDay);

        //        if (isAny1 || isAny2) continue;

        //        var time = item.update_time.AddMinutes(-3);
        //        var dataListTimeQ = dataList.Where(x => x.create_time >= time && x.create_time < item.update_time);

        //        var time1 = item.update_time.AddMinutes(-1);
        //        var minutes1Data = dataList.Where(x => x.create_time >= time1 && x.create_time < item.update_time).OrderBy(x => x.create_time);

        //        var ysj1pl = minutes1Data.Any(x => x.jz1ysj1pl != 50);
        //        var ysj2pl = minutes1Data.Any(x => x.jz1ysj2pl != 50);

        //        var P50 = 0.35565 + 0.05022 * item.jz1swwd;

        //        var ysj1P = (item.jz1ysj1gyyl / 1000) > (P50 * 1.1);
        //        var ysj2P = (item.jz1ysj2gyyl / 1000) > (P50 * 1.1);

        //        var isTrue1 = !dataListTimeQ.Any(x => x.jz1ysj1zt == 0) && ysj1P;
        //        var isTrue2 = !dataListTimeQ.Any(x => x.jz1ysjj2zt == 0) && ysj2P;

        //        if (isTrue1 || isTrue2)
        //        {
        //            var addFault = new FaultOrWarn
        //            {
        //                xlh = XL,
        //                lch = item.lch,
        //                cxh = item.cxh,
        //                DeviceCode = item.device_code,
        //                Code = Warn1.FaultCode,
        //                Name = Warn1.FaultName,
        //                FaultType = Warn1.Type,
        //                Type = "4",
        //                State = "1",
        //                createtime = item.update_time
        //            };
        //            addFaults.Add(addFault);

        //            var count = faultData.Where(x => x.Type == "4").Count();

        //            if (count >= 9)
        //            {
        //                addFault.Type = "2";
        //                addFaults.Add(addFault);
        //                setFaults.Add(addFault);
        //            }

        //            if (count >= 14)
        //            {
        //                addFault.Type = "3";
        //                addFault.Name = Warn1.FaultName[..^2] + "报警";
        //                addFaults.Add(addFault);
        //                setFaults.Add(addFault);
        //            }
        //        }   
        //    }

        //    var faultReq = await FaultSetHttpPost(setFaults, new List<FaultOrWarn>());

        //    if (faultReq != null && faultReq.result_code == "200")
        //    {
        //        _logger.LogInformation($"冷凝进出风短路预警推送成功，新增了{setFaults.Count}条预警");

        //        for (int i = 0; i < addFaults.Count; i++)
        //        {
        //            addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
        //        }
        //    }

        //    if (addFaults.Count > 0)
        //    {
        //        var addnum = _db.Insertable(addFaults).ExecuteCommand();


        //        _logger.LogInformation($"冷凝进出风短路预警同步完成，新增了{addnum}条预警");
        //    }
        //}

        ///// <summary>
        ///// 冷凝进出风短路预警模型
        ///// </summary>
        ///// <returns></returns>
        //private async Task GetLnjcfdlFault()
        //{
        //    EquipmentFault Warn1;
        //    var addFaults = new List<FaultOrWarn>();

        //    var config = _db.Queryable<SYS_CONFIG>().ToList();
        //    var nowData = _db.Queryable<TB_PARSING_NEWDATAS>().ToList();
        //    var equipments = await _db.Queryable<EquipmentFault>().ToListAsync();
        //    //获取线路名称
        //    var XL = config.Where(x => x.concode == _lineCode).First().conval;

        //    var newData = await GetNewData(null);
        //    if (newData.Count == 0) return;

        //    foreach (var item in nowData)
        //    {

        //        if (!new[] { 2, 3, 4, 5 }.Contains(item.tfms)) continue;

        //        if (item.yxtzjid == 1)
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014190001");
        //        }
        //        else
        //        {
        //            Warn1 = equipments.First(x => x.FaultCode == "hvac1014190002");
        //        }

        //        var dataList = newData.Where(x => x.device_code == item.device_code).ToList();
        //        var isAny1 = _db.Queryable<FaultOrWarn>().First(x => x.DeviceCode == item.device_code && x.Code == Warn1.FaultCode && x.State == "1");

        //        var time = item.update_time.AddMinutes(-3);
        //        var dataListTimeQ = dataList.Where(x => x.create_time >= time && x.create_time < item.update_time);

        //        var time1 = item.update_time.AddMinutes(-1);
        //        var minutes1Data = dataList.Where(x => x.create_time >= time1 && x.create_time < item.update_time).OrderBy(x => x.create_time);

        //        var ysj1pl = minutes1Data.Any(x => x.jz1ysj1pl != minutes1Data.First().jz1ysj1pl);
        //        var ysj2pl = minutes1Data.Any(x => x.jz1ysj2pl != minutes1Data.First().jz1ysj2pl);

        //        var ylc = minutes1Data.Last().jz1ysj1gyyl - minutes1Data.First().jz1ysj1gyyl;
        //        var ylc2 = minutes1Data.Last().jz1ysj2gyyl - minutes1Data.First().jz1ysj2gyyl;

        //        var isTrue1 = !dataListTimeQ.Any(x => x.jz1ysj1zt == 0) && !ysj1pl && ylc > 200;
        //        var isTrue2 = !dataListTimeQ.Any(x => x.jz1ysjj2zt == 0) && !ysj2pl && ylc2 > 200;

        //        if (isTrue1 || isTrue2)
        //        {
        //            var addFault = new FaultOrWarn
        //            {
        //                xlh = XL,
        //                lch = item.lch,
        //                cxh = item.cxh,
        //                DeviceCode = item.device_code,
        //                Code = Warn1.FaultCode,
        //                Name = Warn1.FaultName,
        //                FaultType = Warn1.Type,
        //                Type = "2",
        //                State = "1",
        //                createtime = item.update_time
        //            };
        //            addFaults.Add(addFault);
        //        }           
        //    }

        //    var faultReq = await FaultSetHttpPost(addFaults, new List<FaultOrWarn>());

        //    if (faultReq != null && faultReq.result_code == "200")
        //    {
        //        _logger.LogInformation($"冷凝进出风短路预警推送成功，新增了{addFaults.Count}条预警");

        //        for (int i = 0; i < addFaults.Count; i++)
        //        {
        //            addFaults[i].SendRepId = faultReq.result_data.new_faults[i];
        //        }
        //    }

        //    if (addFaults.Count > 0)
        //    {
        //        var addnum = _db.Insertable(addFaults).ExecuteCommand();
        //        _logger.LogInformation($"冷凝进出风短路预警同步完成，新增了{addnum}条预警");
        //    }
        //}

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





