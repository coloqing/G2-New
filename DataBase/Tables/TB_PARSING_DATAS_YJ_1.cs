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
    [SugarTable("TB_PARSING_DATAS_YJ_1_{year}{month}{day}")]
    public partial class TB_PARSING_DATAS_YJ_1
    {
           public TB_PARSING_DATAS_YJ_1(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnDescription="")]
           public long id {get;set;}

        /// <summary>
        /// Desc:列车号和网络MVB协议中列车号保持一致
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "列车号和网络MVB协议中列车号保持一致", Length=50)]
           public string lch {get;set;}

        /// <summary>
        /// Desc:车厢号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "车厢号", Length=50)]
           public string cxh {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="")]
           public long? ybwid {get;set;}

        /// <summary>
        /// Desc:设备编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "设备编码", Length=50)]
           public string device_code {get;set;}

        /// <summary>
        /// Desc:机组1湿度值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1湿度值", Length=5)]
           public string jz1sdz {get;set;}

        /// <summary>
        /// Desc:机组1湿度值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1湿度值", Length=5)]
           public string jz2sdz {get;set;}

        /// <summary>
        /// Desc:机组1CO2值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1CO2值", Length=5)]
           public string jz1co2z {get;set;}

        /// <summary>
        /// Desc:机组2湿度值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2湿度值", Length=10)]
           public string jz2co2z {get;set;}

        /// <summary>
        /// Desc:机组1送回风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1送回风温度", Length = 8)]
           public string jz1shfwd {get;set;}

        /// <summary>
        /// Desc:机组2送回风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2送回风温度", Length = 8)]
           public string jz2shfwd {get;set;}



        /// <summary>
        /// Desc:机组1制冷系统1低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1制冷系统1低压传感器故障", Length=1)]
           public string jz1zlxt1dycgqgz {get;set;}

        /// <summary>
        /// Desc:机组1制冷系统1高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1制冷系统1高压传感器故障", Length=1)]
           public string jz1zlxt1gycgqgz {get;set;}

        /// <summary>
        /// Desc:机组1制冷系统2低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1制冷系统2低压传感器故障", Length=1)]
           public string jz1zlxt2dycgqgz {get;set;}

        /// <summary>
        /// Desc:机组1制冷系统2高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1制冷系统2高压传感器故障", Length=1)]
           public string jz1zlxt2gycgqgz {get;set;}

        /// <summary>
        /// Desc:机组2制冷系统1低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2制冷系统1低压传感器故障", Length=1)]
           public string jz2zlxt1dycgqgz {get;set;}

        /// <summary>
        /// Desc:机组2制冷系统1高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2制冷系统1高压传感器故障", Length=1)]
           public string jz2zlxt1gycgqgz {get;set;}

        /// <summary>
        /// Desc:机组2制冷系统2低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2制冷系统2低压传感器故障", Length=1)]
           public string jz2zlxt2dycgqgz {get;set;}

        /// <summary>
        /// Desc:机组2制冷系统2高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2制冷系统2高压传感器故障", Length=1)]
           public string jz2zlxt2gycgqgz {get;set;}

        /// <summary>
        /// Desc:机组1新风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1新风温度", Length = 8)]
           public string jz1xfwd {get;set;}

        /// <summary>
        /// Desc:机组1回送风温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1回送风温度1", Length = 8)]
           public string jz1hsfwd1 {get;set;}

        /// <summary>
        /// Desc:机组1回送风温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1回送风温度2", Length = 8)]
           public string jz1hsfwd2 {get;set;}

        /// <summary>
        /// Desc:机组2新风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2新风温度", Length=10)]
           public string jz2xfwd {get;set;}

        /// <summary>
        /// Desc:机组2送风温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2送风温度1", Length = 8)]
           public string jz2hsfwd1 {get;set;}

        /// <summary>
        /// Desc:机组2送风温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2送风温度2", Length = 8)]
           public string jz2hsfwd2 {get;set;}




        /// <summary>
        /// Desc:机组1制冷系统1泄漏预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1制冷系统1泄漏预警", Length=1)]
           public string jz1zlxt1xlyj {get;set;}

        /// <summary>
        /// Desc:机组1制冷系统2泄漏预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组1制冷系统2泄漏预警", Length=1)]
           public string jz1zlxt2xlyj {get;set;}

        /// <summary>
        /// Desc:机组2制冷系统1泄漏预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2制冷系统1泄漏预警", Length=1)]
           public string jz2zlxt1xlyj {get;set;}

        /// <summary>
        /// Desc:机组2制冷系统2泄漏预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription= "机组2制冷系统2泄漏预警", Length=1)]
           public string jz2zlxt2xlyj {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="",Length=50)]
           public string rq {get;set; }

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
        [SugarColumn(ColumnDescription="")]
           public DateTime create_time {get;set;}

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
