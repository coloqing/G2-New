using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAFKA_PARSE.DTO
{
    /// <summary>
    /// 列车信息
    /// </summary>
    [SugarTable("TB_Train")]
    public class TB_Train
    {
        /// <summary>
        /// 源设备号
        /// </summary>
        [SugarColumn(ColumnDescription = "源设备号")]
        public int bwsb_0 { get; set; }
        /// <summary>
        /// 宿设备号
        /// </summary>
        [SugarColumn(ColumnDescription = "宿设备号")]
        public int bwsb_1 { get; set; }
        /// <summary>
        /// WTS 设备号
        /// </summary>
        [SugarColumn(ColumnDescription = "WTS 设备号")]
        public int bwsb_2 { get; set; }
        /// <summary>
        /// 报文类型
        /// </summary>
        [SugarColumn(ColumnDescription = "报文类型")]
        public int bwsb_3 { get; set; }
        /// <summary>
        /// 数据帧号
        /// </summary>
        [SugarColumn(ColumnDescription = "数据帧号")]
        public int sjzh { get; set; }
        /// <summary>
        /// 城市号
        /// </summary>
        [SugarColumn(ColumnDescription = "城市号")]
        public int csh { get; set; }
        /// <summary>
        /// 线路号
        /// </summary>
        [SugarColumn(ColumnDescription = "线路号")]
        public int xlh { get; set; }
        /// <summary>
        /// 列车车型
        /// </summary>
        [SugarColumn(ColumnDescription = "列车车型")]
        public int lccx { get; set; }
        /// <summary>
        /// 列车号
        /// </summary>
        [SugarColumn(ColumnDescription = "列车号")]
        public int lch { get; set; }
        /// <summary>
        /// 车厢号
        /// </summary>
        [SugarColumn(ColumnDescription = "车厢号")]
        public int cxh { get; set; }
        /// <summary>
        /// 协议版本号
        /// </summary>
        [SugarColumn(ColumnDescription = "协议版本号")]
        public int xybbh { get; set; }

        /// <summary>
        /// 是否需要应答
        /// </summary>
        [SugarColumn(ColumnDescription = "是否需要应答")]
        public int sfxyyd { get; set; }
        /// <summary>
        /// 通道标识
        /// </summary>
        [SugarColumn(ColumnDescription = "通道标识")]
        public int tdbs { get; set; }
        /// <summary>
        /// 库内标识位
        /// </summary>
        [SugarColumn(ColumnDescription = "库内标识位")]
        public int knbsw { get; set; }
        /// <summary>
        /// 数据源设备名称标识
        /// </summary>
        [SugarColumn(ColumnDescription = "数据源设备名称标识")]
        public int sjysbmcbs { get; set; }

        public DateTime DataSoursceTime { get; set; }

        public DateTime WTSTime { get; set; }
        public long id { get; set; }

    }
}
