﻿//using AutoMapper;
//using DataBase.Tables;
//using KAFKA_PARSE.DTO;
//using Microsoft.Extensions.Hosting;
//using SqlSugar;
//using SqlSugar.SplitTableExtensions;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KAFKA_PARSE
//{
//    public class DataCacheService : BackgroundService
//    {
//        private readonly MyDbContext dbContext;
//        static ConcurrentQueue<PartsLife> _PartsLife = new ConcurrentQueue<PartsLife>();
//        static ConcurrentQueue<TB_PARSING_DATAS> _TB_PARSING_DATAS = new ConcurrentQueue<TB_PARSING_DATAS>();
//        static ConcurrentQueue<TB_PARSING_NEWDATAS> _TB_PARSING_NEWDATAS_Update = new ConcurrentQueue<TB_PARSING_NEWDATAS>();
//        static ConcurrentQueue<TB_PARSING_NEWDATAS> _TB_PARSING_NEWDATAS = new ConcurrentQueue<TB_PARSING_NEWDATAS>();
//        static ConcurrentQueue<TB_YSBW> _TB_YSBW = new ConcurrentQueue<TB_YSBW>();

//        public DataCacheService(MyDbContext dbContext)
//        {
//            this.dbContext = dbContext;
//            SnowFlakeSingle.WorkId = 0;
//        }
//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {

//                var plList = new List<PartsLife>();
//                for (var i = 0; i < _PartsLife.Count; i++)
//                {
//                    if (_PartsLife.TryDequeue(out var data))
//                    {
//                        plList.Add(data);
//                    }
//                }

//                var tpdList = new List<TB_PARSING_DATAS>();
//                for (var i = 0; i < _TB_PARSING_DATAS.Count; i++)
//                {
//                    if (_TB_PARSING_DATAS.TryDequeue(out var data))
//                    {
//                        tpdList.Add(data);
//                    }
//                }

//                var tpnUpdateList = new List<TB_PARSING_NEWDATAS>();
//                for (var i = 0; i < _TB_PARSING_NEWDATAS_Update.Count; i++)
//                {
//                    if (_TB_PARSING_NEWDATAS_Update.TryDequeue(out var data))
//                    {
//                        tpnUpdateList.Add(data);
//                    }
//                }

//                var tpnList = new List<TB_PARSING_NEWDATAS>();
//                for (var i = 0; i < _TB_PARSING_NEWDATAS.Count; i++)
//                {
//                    if (_TB_PARSING_NEWDATAS.TryDequeue(out var data))
//                    {
//                        tpnList.Add(data);
//                    }
//                }

//                var ysbw = new List<TB_YSBW>();
//                for (var i = 0; i < _TB_YSBW.Count; i++)
//                {
//                    if (_TB_YSBW.TryDequeue(out var data))
//                    {
//                        ysbw.Add(data);
//                    }
//                }

//                Stopwatch stopwatch = Stopwatch.StartNew();

//                try
//                {
//                    dbContext.Fastest<PartsLife>().PageSize(plList.Count).BulkCopy(plList);
//                    dbContext.Fastest<TB_PARSING_DATAS>().PageSize(tpdList.Count).SplitTable().BulkCopy(tpdList);
//                    dbContext.Fastest<TB_PARSING_NEWDATAS>().PageSize(tpnList.Count).BulkCopy(tpnList);
//                    dbContext.Fastest<TB_PARSING_NEWDATAS>().BulkUpdate(tpnUpdateList);
//                    dbContext.Fastest<TB_YSBW>().PageSize(ysbw.Count).SplitTable().BulkCopy(ysbw);
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e.Message);
//                }

//                stopwatch.Stop();
//                Console.WriteLine("====================================");
//                Console.WriteLine($"{plList.Count}  {tpdList.Count} {tpnList.Count} {tpnUpdateList.Count} {ysbw.Count}");
//                Console.WriteLine($"插入耗时 == {stopwatch.ElapsedMilliseconds}");
//                Console.WriteLine("====================================");

//                await Task.Delay(5000, stoppingToken);
//            }
//        }
//        public static void AddPartsLifes(List<PartsLife> list)
//        {
//            foreach (var item in list)
//            {
//                _PartsLife.Enqueue(item);
//            }
//        }

//        public static void AddTB_PARSING_DATAS(List<TB_PARSING_DATAS> list)
//        {
//            foreach (var item in list)
//            {
//                _TB_PARSING_DATAS.Enqueue(item);
//            }
//        }

//        public static void AddUpdateTB_PARSING_NEWDATAS(List<TB_PARSING_NEWDATAS> list)
//        {
//            foreach (var item in list)
//            {
//                _TB_PARSING_NEWDATAS_Update.Enqueue(item);
//            }
//        }

//        public static void AddTB_PARSING_NEWDATAS(List<TB_PARSING_NEWDATAS> list)
//        {
//            foreach (var item in list)
//            {
//                _TB_PARSING_NEWDATAS.Enqueue(item);
//            }
//        }

//        public static void AddTB_YSBW(TB_YSBW data)
//        {
//            _TB_YSBW.Enqueue(data);
//        }

//    }
//}
