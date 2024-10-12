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
    [SugarTable("TB_PARSING_DATAS_YJ_2_{year}{month}{day}")]
    public partial class TB_PARSING_DATAS_YJ_2
    {
        public TB_PARSING_DATAS_YJ_2()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true,  ColumnDescription = "")]
        public long id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "", Length = 50)]
        public string lch { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "", Length = 50)]
        public string cxh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "")]
        public long? ybwid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "", Length = 50)]
        public string device_code { get; set; }

        /// <summary>
        /// Desc:机组1新风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1新风温度", Length = 8)]
        public string jz1xfwd { get; set; }

        /// <summary>
        /// Desc:机组1回送风温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回送风温度1", Length = 8)]
        public string jz1hsfwd1 { get; set; }

        /// <summary>
        /// Desc:机组1回送风温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回送风温度2", Length = 8)]
        public string jz1hsfwd2 { get; set; }

        /// <summary>
        /// Desc:机组2新风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2新风温度", Length = 10)]
        public string jz2xfwd { get; set; }

        /// <summary>
        /// Desc:机组2送风温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2送风温度1", Length = 8)]
        public string jz2hsfwd1 { get; set; }

        /// <summary>
        /// Desc:机组2送风温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2送风温度2", Length = 8)]
        public string jz2hsfwd2 { get; set; }

        /// <summary>
        /// Desc:机组1送回风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1送回风温度", Length = 10)]
        public string jz1shfwd { get; set; }

        /// <summary>
        /// Desc:机组2送回风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2送回风温度", Length = 10)]
        public string jz2shfwd { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1运行", Length = 1)]
        public string jz1lnfj1yx { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2运行", Length = 1)]
        public string jz1lnfj2yx { get; set; }

        /// <summary>
        /// Desc:机组1压缩机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1运行", Length = 1)]
        public string jz1ysj1yx { get; set; }

        /// <summary>
        /// Desc:机组1压缩机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2运行", Length = 1)]
        public string jz1ysj2yx { get; set; }


        /// <summary>
        /// Desc:机组2冷凝风机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机1运行", Length = 1)]
        public string jz2lnfj1yx { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机2运行", Length = 1)]
        public string jz2lnfj2yx { get; set; }

        /// <summary>
        /// Desc:机组2压缩机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1运行", Length = 1)]
        public string jz2ysj1yx { get; set; }

        /// <summary>
        /// Desc:机组2压缩机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2运行", Length = 1)]
        public string jz2ysj2yx { get; set; }

        /// <summary>
        /// Desc:机组1紫外线灯运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1紫外线灯运行", Length = 1)]
        public string jz1zwxdyx { get; set; }

        /// <summary>
        /// Desc:机组2紫外线灯运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2紫外线灯运行", Length = 1)]
        public string jz2zwxdyx { get; set; }


        /// <summary>
        /// Desc:机组1蒸发风机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机1电流", Length = 10)]
        public string jz1zffj1dl { get; set; }

        /// <summary>
        /// Desc:机组1蒸发风机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机2电流", Length = 10)]
        public string jz1zffj2dl { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1电流", Length = 10)]
        public string jz1lnfj1dl { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2电流", Length = 10)]
        public string jz1lnfj2dl { get; set; }

        /// <summary>
        /// Desc:机组1压缩机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1电流", Length = 10)]
        public string jz1ysj1dl { get; set; }

        /// <summary>
        /// Desc:机组1压缩机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2电流", Length = 10)]
        public string jz1ysj2dl { get; set; }



        /// <summary>
        /// Desc:机组2蒸发风机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机1电流", Length = 10)]
        public string jz2zffj1dl { get; set; }

        /// <summary>
        /// Desc:机组2蒸发风机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机2电流", Length = 10)]
        public string jz2zffj2dl { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机1电流", Length = 10)]
        public string jz2lnfj1dl { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机2电流", Length = 10)]
        public string jz2lnfj2dl { get; set; }

        /// <summary>
        /// Desc:机组2压缩机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1电流", Length = 10)]
        public string jz2ysj1dl { get; set; }

        /// <summary>
        /// Desc:机组2压缩机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2电流", Length = 10)]
        public string jz2ysj2dl { get; set; }





        /// <summary>
        /// Desc:机组1制冷系统1高压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1高压压力值", Length = 10)]
        public string jz1zlxt1gyylz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2高压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2高压压力值", Length = 10)]
        public string jz1zlxt2gyylz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1高压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1高压压力值", Length = 10)]
        public string jz2zlxt1gyylz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统2高压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2高压压力值", Length = 10)]
        public string jz2zlxt2gyylz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1低压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1低压压力值", Length = 10)]
        public string jz1zlxt1dyylz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2低压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2低压压力值", Length = 10)]
        public string jz1zlxt2dyylz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1低压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1低压压力值", Length = 10)]
        public string jz2zlxt1dyylz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统2低压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2低压压力值", Length = 10)]
        public string jz2zlxt2dyylz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1低压传感器故障", Length = 1)]
        public string jz1zlxt1dycgqgz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1高压传感器故障", Length = 1)]
        public string jz1zlxt1gycgqgz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2低压传感器故障", Length = 1)]
        public string jz1zlxt2dycgqgz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2高压传感器故障", Length = 1)]
        public string jz1zlxt2gycgqgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1低压传感器故障", Length = 1)]
        public string jz2zlxt1dycgqgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1高压传感器故障", Length = 1)]
        public string jz2zlxt1gycgqgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统2低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2低压传感器故障", Length = 1)]
        public string jz2zlxt2dycgqgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统2高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2高压传感器故障", Length = 1)]
        public string jz2zlxt2gycgqgz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "", Length = 50)]
        public string rq { get; set; }

        /// <summary>
        /// Desc:年月日时分秒.毫秒
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "年月日时分秒.毫秒")]
        [SplitField]
        public DateTime rqDateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "")]
        public DateTime create_time { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "", Length = 50)]
        public string timestamp { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "", Length = 50)]
        public string offset { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "", Length = 50)]
        public string partition { get; set; }
        /// <summary>
        /// Desc:机组1目标温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1目标温度", Length = 10)]
        public string jz1mbwd { get; set; }
        /// <summary>
        /// Desc:机组2目标温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2目标温度", Length = 10)]
        public string jz2mbwd { get; set; }
        /// <summary>
        /// Desc:机组2蒸发风机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机2运行", Length = 1)]
        public string jz2zffj2yx { get; set; }
        /// <summary>
        /// Desc:机组2蒸发风机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机1运行", Length = 1)]
        public string jz2zffj1yx { get; set; }
        /// <summary>
        /// Desc:机组1蒸发风机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机2运行", Length = 1)]
        public string jz1zffj2yx { get; set; }

        /// <summary>
        /// Desc:机组1蒸发风机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机1运行", Length = 1)]
        public string jz1zffj1yx { get; set; }
    }
}
