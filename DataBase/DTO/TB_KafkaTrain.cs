using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAFKA_PARSE.DTO
{
    public class TB_KafkaTrain
    {
        /// <summary>
        /// 报文头特征码
        /// </summary>
        [SugarColumn(ColumnDescription = "报文头特征码")]
        public int bwttzm_0 { get; set; }
        /// <summary>
        /// 报文长度
        /// </summary>
        [SugarColumn(ColumnDescription = "报文长度")]
        public int bwcd { get; set; }
        /// <summary>
        /// 数据帧号-须等于报文去的帧号
        /// </summary>
        [SugarColumn(ColumnDescription = "数据帧号-须等于报文去的帧号")]
        public int zh { get; set; }
        /// <summary>
        /// 协议版本号 此版本号 0x01
        /// </summary>
        [SugarColumn(ColumnDescription = "协议版本号 此版本号 0x01")]
        public int xybb { get; set; }
        /// <summary>
        /// 加密类型
        /// </summary>
        [SugarColumn(ColumnDescription = "加密类型")]
        public int jmlx { get; set; }
        /// <summary>
        /// 压缩类型
        /// </summary>
        [SugarColumn(ColumnDescription = "压缩类型")]
        public int yslx { get; set; }
        /// <summary>
        /// 有效长度
        /// </summary>
        [SugarColumn(ColumnDescription = "有效长度")]
        public int yxcd { get; set; }
        /// <summary>
        /// 预留
        /// </summary>
        [SugarColumn(ColumnDescription = "预留")]
        public int yl { get; set; }

        /// <summary>
        /// 帧头0x425A
        /// </summary>
        [SugarColumn(ColumnDescription = "帧头0x425A")]
        public int tzzj_0 { get; set; }
        /// <summary>
        /// 数据区报文长度
        /// </summary>
        [SugarColumn(ColumnDescription = "数据区报文长度")]
        public int sjqbwcd { get; set; }
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
        /// WTS时间
        /// </summary>
        [SugarColumn(ColumnDescription = "WTS时间")]
        public int yyyy { get; set; }
        /// <summary>
        /// WTS时间
        /// </summary>
        [SugarColumn(ColumnDescription = "WTS时间")]
        public int MM { get; set; }
        /// <summary>
        /// WTS时间
        /// </summary>
        [SugarColumn(ColumnDescription = "WTS时间")]
        public int dd { get; set; }
        /// <summary>
        /// WTS时间
        /// </summary>
        [SugarColumn(ColumnDescription = "WTS时间")]
        public int HH { get; set; }
        /// <summary>
        /// WTS时间
        /// </summary>
        [SugarColumn(ColumnDescription = "WTS时间")]
        public int mm { get; set; }
        /// <summary>
        /// WTS时间
        /// </summary>
        [SugarColumn(ColumnDescription = "WTS时间")]
        public int ss { get; set; }
        /// <summary>
        /// WTS时间
        /// </summary>
        [SugarColumn(ColumnDescription = "WTS时间")]
        public int fff1 { get; set; }
        /// <summary>
        /// WTS时间
        /// </summary>
        [SugarColumn(ColumnDescription = "WTS时间")]
        public int fff2 { get; set; }
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

        /// <summary>
        /// 数据源时间
        /// </summary>
        [SugarColumn(ColumnDescription = "数据源时间")]
        public int Syyyy { get; set; }
        /// <summary>
        /// 数据源时间
        /// </summary>
        [SugarColumn(ColumnDescription = "数据源时间")]
        public int SMM { get; set; }
        /// <summary>
        /// 数据源时间
        /// </summary>
        [SugarColumn(ColumnDescription = "数据源时间")]
        public int Sdd { get; set; }
        /// <summary>
        /// 数据源时间
        /// </summary>
        [SugarColumn(ColumnDescription = "数据源时间")]
        public int SHH { get; set; }
        /// <summary>
        /// 数据源时间
        /// </summary>
        [SugarColumn(ColumnDescription = "数据源时间")]
        public int Smm { get; set; }
        /// <summary>
        /// 数据源时间
        /// </summary>
        [SugarColumn(ColumnDescription = "数据源时间")]
        public int Sss { get; set; }
        /// <summary>
        /// 数据源时间
        /// </summary>
        [SugarColumn(ColumnDescription = "数据源时间")]
        public int Sfff1 { get; set; }

        /// <summary>
        /// 数据源时间
        /// </summary>
        [SugarColumn(ColumnDescription = "数据源时间")]
        public int Sfff2 { get; set; }

        public DateTime DataSoursceTime { get; set; }

        public DateTime WTSTime { get; set; }
        public long id { get; set; }

    }
}
