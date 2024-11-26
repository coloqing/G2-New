//using DataBase.Tables;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KAFKA_PARSE.WarningSystem
//{
//    public class CompressorWarning
//    {
//        private const double HighPressureThreshold = 2.9;
//        private const double LowPressureThreshold = 0.15;
//        private const int WarmUpPeriodSeconds = 120; // 压缩机开机前2分钟（120秒）
//        private const int WarningDurationSeconds = 180; // 预警持续3分钟（180秒）
//        private const int WarningCountThreshold = 3; // 一小时内出现3次预警则上报
//        private const int MaxWarningReportsPerDay = 1; // 每天最多上报一次

//        private readonly List<TB_PARSING_DATAS_NEWCS> _pressureDataQueue = new List<TB_PARSING_DATAS_NEWCS>();
//        private DateTime _lastWarningReportTime;
//        private int _warningCountInHour;

//        public CompressorWarning()
//        {
//            _lastWarningReportTime = DateTime.MinValue; // 初始化为最小值，表示尚未报告过
//            _warningCountInHour = 0; // 初始化预警计数为0
//        }

//        public void ProcessNewData(TB_PARSING_DATAS_NEWCS newData)
//        {
//            // 移除超出3分钟窗口的数据
//            _pressureDataQueue.RemoveAll(data => (newData.Timestamp - data.Timestamp).TotalSeconds > WarningDurationSeconds);

//            // 如果压缩机刚启动，则跳过数据
//            if (!newData.IsCompressorStarted || (newData.Timestamp - GetCompressorStartTime(newData)).TotalSeconds < WarmUpPeriodSeconds)
//            {
//                _pressureDataQueue.Add(newData); // 仍然添加到队列中，但不进行预警检查
//                return;
//            }

//            // 检查预警条件
//            if (IsHighPressureWarningConditionMet(newData) || IsLowPressureWarningConditionMet(newData))
//            {
//                _warningCountInHour++;

//                // 如果一小时内预警次数达到阈值，并且今天还没有上报过，则上报预警
//                if (_warningCountInHour >= WarningCountThreshold &&
//                    (DateTime.Now - _lastWarningReportTime).TotalDays >= 1)
//                {
//                    ReportWarning();
//                    _lastWarningReportTime = DateTime.Now; // 更新最后上报时间
//                    _warningCountInHour = 0; // 重置一小时内预警计数
//                }
//            }
//            else
//            {
//                // 如果当前数据不满足预警条件，则重置一小时内预警计数（可选）
//                // 这取决于您是否希望在一个预警事件结束后重新开始计数
//                // _warningCountInHour = 0; // 如果需要，可以取消注释这行代码
//            }

//            // 添加到队列中（这里其实已经通过上面的逻辑处理过了，但为了保持数据结构完整性，还是加上）
//            _pressureDataQueue.Add(newData);
//        }

//        private DateTime GetCompressorStartTime(TB_PARSING_DATAS_NEWCS data)
//        {
//            // 这里需要实现一个逻辑来获取压缩机的实际启动时间
//            // 可能是通过查询数据库中的历史记录，或者是通过其他方式（如设备状态变化日志）
//            // 由于示例中未提供此信息，因此这里简单返回当前时间（实际上需要替换为实际逻辑）
//            return data.Timestamp; // 这是一个占位符，需要替换为实际的压缩机启动时间
//        }

//        private bool IsHighPressureWarningConditionMet(TB_PARSING_DATAS_NEWCS newData)
//        {
//            return !newData.IsPressureSensorFaulty &&
//                   _pressureDataQueue.All(data => (data.Timestamp - newData.Timestamp).TotalSeconds <= WarningDurationSeconds &&
//                                                  data.HighPressure > HighPressureThreshold &&
//                                                  data.IsCoolingMode);
//        }

//        private bool IsLowPressureWarningConditionMet(TB_PARSING_DATAS_NEWCS newData)
//        {
//            return !newData.IsPressureSensorFaulty &&
//                   _pressureDataQueue.All(data => (data.Timestamp - newData.Timestamp).TotalSeconds <= WarningDurationSeconds &&
//                                                  data.LowPressure <= LowPressureThreshold &&
//                                                  data.IsCoolingMode);
//        }

//        private void ReportWarning()
//        {
//            // 这里实现上报预警的逻辑
//            // 可能是将预警记录插入到数据库中，或者是发送邮件/短信通知等
//            var faultData = new FAULTWARN
//            {
//                WarningTime = DateTime.Now,
//                WarningMessage = "Compressor pressure warning: High pressure > 2.9MPa or Low pressure <= 0.15MPa for over 3 minutes."
//            };

//            // 假设您有一个方法来保存预警记录到数据库
//            _db.Insertable(faultData).ExecuteCommand(); // 这是一个假设的方法调用，需要根据您的数据访问层进行调整
//        }
//    }
//}
