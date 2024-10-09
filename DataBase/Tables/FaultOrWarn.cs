using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Tables
{
    [SugarTable("FaultOrWarn")]
    public class FaultOrWarn
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "主键ID  故障预警表")]
        public int Id { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        [SugarColumn(ColumnDescription = "线路名称")]
        public string xlh { get; set; }

        /// <summary>
        /// 列车号
        /// </summary>
        [SugarColumn(ColumnDescription = "列车号")]
        public string lch { get; set; }
        /// <summary>
        /// 车厢号
        /// </summary>
        public string cxh { get; set; }
        /// <summary>
        /// 设备Code
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 故障编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 故障名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 报警类型 1：故障 2：预警
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 事件状态 0:已关闭 1：未处理
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 报警分类
        /// </summary>
        public string FaultType { get; set; }

        /// <summary>
        /// 上传返回ID
        /// </summary>
        public long SendRepId { get; set; }
   
        public DateTime? createtime { get; set; } = DateTime.Now;
        public DateTime? updatetime { get; set; } = null;

    }
}
