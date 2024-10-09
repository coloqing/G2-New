using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    ///
    ///</summary>
    [SplitTable(SplitType.Day)]
    [SugarTable("TB_PARSING_DATAS_CS_{year}{month}{day}")]
    public partial class TB_PARSING_DATAS_CS
    {
           public TB_PARSING_DATAS_CS(){


           }
           /// <summary>
           /// Desc:主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnDescription="主键ID")]
           public long id {get;set;}

           /// <summary>
           /// Desc:HVAC生命信号H
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="HVAC生命信号H",Length=10)]
           public string hvacsmxhh {get;set;}

           /// <summary>
           /// Desc:HVAC生命信号L
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="HVAC生命信号L",Length=10)]
           public string hvacsmxhl {get;set;}

           /// <summary>
           /// Desc:机组1送回风温度
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1送回风温度",Length=10)]
           public string jz1shfwd {get;set;}

           /// <summary>
           /// Desc:机组1新风温度
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1新风温度",Length=10)]
           public string jz1xfwd {get;set;}

           /// <summary>
           /// Desc:机组1回送风温度1
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1回送风温度1",Length=10)]
           public string jz1hsfwd1 {get;set;}

           /// <summary>
           /// Desc:机组1回送风温度2
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1回送风温度2",Length=10)]
           public string jz1hsfwd2 {get;set;}

           /// <summary>
           /// Desc:机组2送回风温度
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2送回风温度",Length=10)]
           public string jz2shfwd {get;set;}

           /// <summary>
           /// Desc:机组2新风温度
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2新风温度",Length=10)]
           public string jz2xfwd {get;set;}

           /// <summary>
           /// Desc:机组2回送风温度1
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2回送风温度1",Length=10)]
           public string jz2hsfwd1 {get;set;}

           /// <summary>
           /// Desc:机组2回送风温度2
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2回送风温度2",Length=10)]
           public string jz2hsfwd2 {get;set;}

           /// <summary>
           /// Desc:机组1湿度值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1湿度值",Length=10)]
           public string jz1sdz {get;set;}

           /// <summary>
           /// Desc:机组1CO2值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1CO2值",Length=10)]
           public string jz1co2z {get;set;}

           /// <summary>
           /// Desc:机组2湿度值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2湿度值",Length=10)]
           public string jz2sdz {get;set;}

           /// <summary>
           /// Desc:机组2CO2值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2CO2值",Length=10)]
           public string jz2co2z {get;set;}

           /// <summary>
           /// Desc:机组1制冷系统1高压压力值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1制冷系统1高压压力值",Length=10)]
           public string jz1zlxt1gyylz {get;set;}

           /// <summary>
           /// Desc:机组1制冷系统1低压压力值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1制冷系统1低压压力值",Length=10)]
           public string jz1zlxt1dyylz {get;set;}

           /// <summary>
           /// Desc:机组1制冷系统2高压压力值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1制冷系统2高压压力值",Length=10)]
           public string jz1zlxt2gyylz {get;set;}

           /// <summary>
           /// Desc:机组1制冷系统2低压压力值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1制冷系统2低压压力值",Length=10)]
           public string jz1zlxt2dyylz {get;set;}

           /// <summary>
           /// Desc:机组2制冷系统1高压压力值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2制冷系统1高压压力值",Length=10)]
           public string jz2zlxt1gyylz {get;set;}

           /// <summary>
           /// Desc:机组2制冷系统1低压压力值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2制冷系统1低压压力值",Length=10)]
           public string jz2zlxt1dyylz {get;set;}

           /// <summary>
           /// Desc:机组2制冷系统2高压压力值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2制冷系统2高压压力值",Length=10)]
           public string jz2zlxt2gyylz {get;set;}

           /// <summary>
           /// Desc:机组2制冷系统2低压压力值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2制冷系统2低压压力值",Length=10)]
           public string jz2zlxt2dyylz {get;set;}

           /// <summary>
           /// Desc:机组1功耗信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1功耗信息",Length=10)]
           public string jz1ghxx {get;set;}

           /// <summary>
           /// Desc:机组2功耗信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2功耗信息",Length=10)]
           public string jz2ghxx {get;set;}

           /// <summary>
           /// Desc:机组1蒸发风机1电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1蒸发风机1电流",Length=10)]
           public string jz1zffj1dl {get;set;}

           /// <summary>
           /// Desc:机组1蒸发风机2电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1蒸发风机2电流",Length=10)]
           public string jz1zffj2dl {get;set;}

           /// <summary>
           /// Desc:机组1冷凝风机1电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1冷凝风机1电流",Length=10)]
           public string jz1lnfj1dl {get;set;}

           /// <summary>
           /// Desc:机组1冷凝风机2电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1冷凝风机2电流",Length=10)]
           public string jz1lnfj2dl {get;set;}

           /// <summary>
           /// Desc:机组1压缩机1电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1压缩机1电流",Length=10)]
           public string jz1ysj1dl {get;set;}

           /// <summary>
           /// Desc:机组1压缩机2电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1压缩机2电流",Length=10)]
           public string jz1ysj2dl {get;set;}

           /// <summary>
           /// Desc:机组2蒸发风机1电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2蒸发风机1电流",Length=10)]
           public string jz2zffj1dl {get;set;}

           /// <summary>
           /// Desc:机组2蒸发风机2电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2蒸发风机2电流",Length=10)]
           public string jz2zffj2dl {get;set;}

           /// <summary>
           /// Desc:机组2冷凝风机1电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2冷凝风机1电流",Length=10)]
           public string jz2lnfj1dl {get;set;}

           /// <summary>
           /// Desc:机组2冷凝风机2电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2冷凝风机2电流",Length=10)]
           public string jz2lnfj2dl {get;set;}

           /// <summary>
           /// Desc:机组2压缩机1电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2压缩机1电流",Length=10)]
           public string jz2ysj1dl {get;set;}

           /// <summary>
           /// Desc:机组2压缩机2电流
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2压缩机2电流",Length=10)]
           public string jz2ysj2dl {get;set;}

        ///// <summary>
        ///// Desc:机组1废排风机电流
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[SugarColumn(ColumnDescription="机组1废排风机电流",Length=10)]
        //public string jz1fpfjdl {get;set;}

        ///// <summary>
        ///// Desc:机组2废排风机电流
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[SugarColumn(ColumnDescription="机组2废排风机电流",Length=10)]
        //public string jz2fpfjdl {get;set;}

        ///// <summary>
        ///// Desc:司机室蒸发风机电流
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[SugarColumn(ColumnDescription="司机室蒸发风机电流",Length=10)]
        //public string sjszffjdl {get;set;}

        ///// <summary>
        ///// Desc:司机室冷凝风机电流
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[SugarColumn(ColumnDescription="司机室冷凝风机电流",Length=10)]
        //public string sjslnfjdl {get;set;}

        ///// <summary>
        ///// Desc:司机室压缩机电流
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[SugarColumn(ColumnDescription="司机室压缩机电流",Length=10)]
        //public string sjsysjdl {get;set;}

        /// <summary>
        /// Desc:机组1目标温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1目标温度", Length = 6)]
        public string jz1mbwd { get; set; }
        /// <summary>
        /// Desc:机组2目标温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2目标温度", Length = 6)]
        public string jz2mbwd { get; set; }

        /// <summary>
        /// Desc:机组1控制模式
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription="机组1控制模式",Length=10)]
           public string jz1kzms {get;set;}

           /// <summary>
           /// Desc:机组1工作模式
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1工作模式",Length=10)]
           public string jz1gzms {get;set;}

           /// <summary>
           /// Desc:机组2控制模式
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2控制模式",Length=10)]
           public string jz2kzms {get;set;}

           /// <summary>
           /// Desc:机组2工作模式
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2工作模式",Length=10)]
           public string jz2gzms {get;set;}

           ///// <summary>
           ///// Desc:司机室工作模式【仅头车】
           ///// Default:
           ///// Nullable:True
           ///// </summary>           
           //[SugarColumn(ColumnDescription="司机室工作模式【仅头车】",Length=10)]
           //public string sjsgzms {get;set;}

           /// <summary>
           /// Desc:年月日时分秒.毫秒
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="年月日时分秒.毫秒",Length=50)]
           public string rq {get;set;}

        /// <summary>
        /// Desc:年月日时分秒.毫秒
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "年月日时分秒.毫秒")]
        public DateTime rqDateTime { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription="创建时间")]
           public DateTime create_time {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="")]
           public long? ybwid {get;set;}

           /// <summary>
           /// Desc:列车号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="列车号",Length=50)]
           public string lch {get;set;}

           /// <summary>
           /// Desc:车厢号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车厢号",Length=50)]
           public string cxh {get;set;}

           /// <summary>
           /// Desc:设备编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="设备编码",Length=50)]
           public string device_code {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="",Length=50)]
           public string timestamp {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="",Length=50)]
           public string offset {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="",Length=50)]
           public string partition {get;set;}

    }
}
