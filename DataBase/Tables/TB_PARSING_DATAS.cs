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
    [SugarTable("TB_PARSING_DATAS_{year}{month}{day}")]
    public partial class TB_PARSING_DATAS
    {
        public TB_PARSING_DATAS()
        {


        }
        /// <summary>
        /// Desc:主键ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键ID")]
        public long id { get; set; }

        /// <summary>
        /// Desc:特征字节 数据头特征码0xAA
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "特征字节 数据头特征码0xAA", Length = 10)]
        public string tzzj_0 { get; set; }

        /// <summary>
        /// Desc:特征字节 数据头特征码0x55
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "特征字节 数据头特征码0x55", Length = 10)]
        public string tzzj_1 { get; set; }

        /// <summary>
        /// Desc:协议版本X(版本号格式X.Y)
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "协议版本X(版本号格式X.Y)", Length = 10)]
        public string xybbx { get; set; }


        /// <summary>
        /// Desc:帧号  周期变化的0~65535
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "帧号  周期变化的0~65535", Length = 5)]
        public string zh { get; set; }


        /// <summary>
        /// Desc:源设备号,参见章节5.1.2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = ":源设备号,参见章节5.1.2", Length = 10)]
        public string ysbh { get; set; }
        /// <summary>
        /// Desc:宿设备号,参见章节5.1.2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = ":宿设备号,参见章节5.1.2", Length = 10)]
        public string ssbh { get; set; }




        /// <summary>
        /// Desc:报文类型,参见章节5.1.4OCS发送到空调系统的心跳报文	7000 空调系统发送到OCS的心跳报文	7001 空调系统发送到OCS的数据报文	7020
        /// Default:
        /// Nullable:True
        /// </summary>         
        [SugarColumn(ColumnDescription = "Desc:报文类型,参见章节5.1.4OCS发送到空调系统的心跳报文 7000 空调系统发送到OCS的心跳报文 7001 空调系统发送到OCS的数据报文 7020", Length = 10)]
        public string bwlx { get; set; }


        /// <summary>
        /// Desc:列车号和网络MVB协议中列车号保持一致
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "列车号和网络MVB协议中列车号保持一致", Length = 50)]
        public string lch { get; set; }

        /// <summary>
        /// Desc:车厢号车厢号序号	设备名称	安装位置	设备号	IP规划 1.	OCS1	A1	40	CN391:10.0.1.162 //2.	OCS2 A2	41	CN391:10.0.6.162//3.	空调 A1	70	MVB协议中为准//4.	空调 B1	71	MVB协议中为准//5.空调 C1	72	MVB协议中为准//6.	空调 C2	73	MVB协议中为准//7.	空调 B2	74	MVB协议中为准//8.	空调 A2	75	MVB协议中为准
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "车厢号序号\t设备名称\t安装位置\t设备号\tIP规划 1.\tOCS1\tA1\t40\tCN391:10.0.1.162 //2.\tOCS2 A2\t41\tCN391:10.0.6.162//3.\t空调 A1\t70\tMVB协议中为准//4.\t空调 B1\t71\tMVB协议中为准//5.空调 C1\t72\tMVB协议中为准//6.\t空调 C2\t73\tMVB协议中为准//7.\t空调 B2\t74\tMVB协议中为准//8.\t空调 A2\t75\tMVB协议中为准", Length = 50)]
        public string cxh { get; set; }

        /// <summary>
        /// Desc:设备编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "设备编码", Length = 50)]
        public string device_code { get; set; }

        /// <summary>
        /// Desc:HVAC生命信号H
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "HVAC生命信号H", Length = 10)]
        public string hvacsmxhh { get; set; }

        /// <summary>
        /// Desc:HVAC生命信号L
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "HVAC生命信号L", Length = 10)]
        public string hvacsmxhl { get; set; }


        /// <summary> 
        /// Desc:软件版本 如0x1234对应显示为V18.3.4 XXXXXXXX ．XXXX ．XXXX
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "软件版本 XXXXXXXX ．XXXX ．XXXX", Length = 20)]
        public string software_version { get; set; }

        /// <summary>
        /// Desc:机组1目标温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1目标温度", Length = 8)]
        public string jz1mbwd { get; set; }
        /// <summary>
        /// Desc:机组2目标温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2目标温度", Length = 8)]
        public string jz2mbwd { get; set; }





        /// <summary>
        /// Desc:机组1送回风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1送回风温度", Length = 8)]
        public string jz1shfwd { get; set; }


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
        /// Desc:机组2送回风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2送回风温度", Length = 8)]
        public string jz2shfwd { get; set; }

        /// <summary>
        /// Desc:机组2新风温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2新风温度", Length = 8)]
        public string jz2xfwd { get; set; }

        /// <summary>
        /// Desc:机组2回送风温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2回送风温度1  机组2送风温度1", Length = 8)]
        public string jz2hsfwd1 { get; set; }

        /// <summary>
        /// Desc:机组2回送风温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2回送风温度2  机组2送风温度2", Length = 8)]
        public string jz2hsfwd2 { get; set; }


        //======对比文档60================================================================================================================================



        /// <summary>
        /// Desc:机组1冷凝温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝温度1", Length = 8)]
        public string jz1lnwd1 { get; set; }
        // <summary>
        /// Desc:机组1冷凝温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝温度2", Length = 8)]
        public string jz1lnwd2 { get; set; }

        /// <summary>
        /// Desc:机组1排气温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1排气温度1", Length = 8)]
        public string jz1pqwd1 { get; set; }
        // <summary>
        /// Desc:机组1冷凝温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1排气温度2", Length = 8)]
        public string jz1pqwd2 { get; set; }
        /// <summary>
        /// Desc:机组1吸气温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1吸气温度1", Length = 8)]
        public string jz1xqwd1 { get; set; }
        // <summary>
        /// Desc:机组1冷凝温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1吸气温度2", Length = 8)]
        public string jz1xqwd2 { get; set; }

        /// <summary>
        /// Desc:机组1冷凝温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝温度1", Length = 8)]
        public string jz2lnwd1 { get; set; }
        // <summary>
        /// Desc:机组1冷凝温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝温度2", Length = 8)]
        public string jz2lnwd2 { get; set; }

        /// <summary>
        /// Desc:机组1排气温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2排气温度1", Length = 8)]
        public string jz2pqwd1 { get; set; }
        // <summary>
        /// Desc:机组1冷凝温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2排气温度2", Length = 8)]
        public string jz2pqwd2 { get; set; }
        /// <summary>
        /// Desc:机组1吸气温度1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2吸气温度1", Length = 8)]
        public string jz2xqwd1 { get; set; }
        // <summary>
        /// Desc:机组1冷凝温度2
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2吸气温度2", Length = 8)]
        public string jz2xqwd2 { get; set; }

        //======对比文档84================================================================================================================================




        /// <summary>
        /// Desc:机组1湿度值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1湿度值", Length = 3)]
        public string jz1sdz { get; set; }

        /// <summary>
        /// Desc:机组1CO2值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1CO2值", Length = 5)]
        public string jz1co2z { get; set; }

        /// <summary>
        /// Desc:机组2湿度值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2湿度值", Length = 3)]
        public string jz2sdz { get; set; }

        /// <summary>
        /// Desc:机组2CO2值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2CO2值", Length = 5)]
        public string jz2co2z { get; set; }

        //======对比文档90================================================================================================================================


        /// <summary>
        /// Desc:机组1制冷系统1高压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1高压压力值", Length = 5)]
        public string jz1zlxt1gyylz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1低压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1低压压力值", Length = 5)]
        public string jz1zlxt1dyylz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2高压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2高压压力值", Length = 5)]
        public string jz1zlxt2gyylz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2低压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2低压压力值", Length = 5)]
        public string jz1zlxt2dyylz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1高压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1高压压力值", Length = 5)]
        public string jz2zlxt1gyylz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1低压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1低压压力值", Length = 5)]
        public string jz2zlxt1dyylz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统2高压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2高压压力值", Length = 5)]
        public string jz2zlxt2gyylz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统2低压压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2低压压力值", Length = 5)]
        public string jz2zlxt2dyylz { get; set; }

        //======对比文档106================================================================================================================================

        /// <summary>
        /// Desc:机组1功耗信息  1=1 KWH（空调总能耗=H*65536+L）
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1功耗信息  1=1 KWH（空调总能耗=H*65536+L）", Length = 5)]
        public string jz1ghxx { get; set; }

        /// <summary>
        /// Desc:机组2功耗信息
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2功耗信息", Length = 5)]
        public string jz2ghxx { get; set; }




        /// <summary>
        /// Desc:机组1蒸发风机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机1电流", Length = 4)]
        public string jz1zffj1dl { get; set; }

        /// <summary>
        /// Desc:机组1蒸发风机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机2电流", Length = 4)]
        public string jz1zffj2dl { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1电流", Length = 4)]
        public string jz1lnfj1dl { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2电流", Length = 4)]
        public string jz1lnfj2dl { get; set; }

        /// <summary>
        /// Desc:机组1压缩机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1电流", Length = 4)]
        public string jz1ysj1dl { get; set; }

        /// <summary>
        /// Desc:机组1压缩机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2电流", Length = 4)]
        public string jz1ysj2dl { get; set; }

        /// <summary>
        /// Desc:机组2蒸发风机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机1电流", Length = 4)]
        public string jz2zffj1dl { get; set; }

        /// <summary>
        /// Desc:机组2蒸发风机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机2电流", Length = 4)]
        public string jz2zffj2dl { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机1电流", Length = 4)]
        public string jz2lnfj1dl { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机2电流", Length = 4)]
        public string jz2lnfj2dl { get; set; }

        /// <summary>
        /// Desc:机组2压缩机1电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1电流", Length = 4)]
        public string jz2ysj1dl { get; set; }

        /// <summary>
        /// Desc:机组2压缩机2电流
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2电流", Length = 4)]
        public string jz2ysj2dl { get; set; }

        //==130hang============================================================================================================
        /// <summary>
        /// Desc:机组1压缩机1电压
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1电压", Length = 4)]
        public string jz1ysj1dy { get; set; }

        /// <summary>
        /// Desc:机组1压缩机2电压
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2电压", Length = 4)]
        public string jz1ysj2dy { get; set; }
        /// <summary>
        /// Desc:机组2压缩机1电压
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1电压", Length = 4)]
        public string jz2ysj1dy { get; set; }

        /// <summary>
        /// Desc:机组2压缩机2电压
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2电压", Length = 4)]
        public string jz2ysj2dy { get; set; }

        /// <summary>
        /// Desc:机组1压缩机1频率
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1频率", Length = 4)]
        public string jz1ysj1pl { get; set; }

        /// <summary>
        /// Desc:机组1压缩机2频率
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2频率", Length = 4)]
        public string jz1ysj2pl { get; set; }
        /// <summary>
        /// Desc:机组2压缩机1频率
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1频率", Length = 4)]
        public string jz2ysj1pl { get; set; }

        /// <summary>
        /// Desc:机组2压缩机2频率
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2频率", Length = 4)]
        public string jz2ysj2pl { get; set; }

        /// <summary>
        /// Desc:机组1压差值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压差值", Length = 4)]
        public string jz1ycz { get; set; }

        /// <summary>
        /// Desc:机组2压差值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压差值", Length = 4)]
        public string jz2ycz { get; set; }


        //=146================================================================================================================
        /// <summary>
        /// Desc:机组1新风阀1开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1新风阀1开度", Length = 3)]
        public string jz1xff1kd { get; set; }
        /// <summary>
        /// Desc:机组1新风阀2开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1新风阀2开度", Length = 3)]
        public string jz1xff2kd { get; set; }

        /// <summary>
        /// Desc:机组1回风阀1开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回风阀1开度", Length = 3)]
        public string jz1hff1kd { get; set; }
        /// <summary>
        /// Desc:机组1回风阀2开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回风阀2开度", Length = 3)]
        public string jz1hff2kd { get; set; }


        /// <summary>
        /// Desc:机组2新风阀1开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2新风阀1开度", Length = 3)]
        public string jz2xff1kd { get; set; }
        /// <summary>
        /// Desc:机组2新风阀2开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2新风阀2开度", Length = 3)]
        public string jz2xff2kd { get; set; }

        /// <summary>
        /// Desc:机组2回风阀1开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2回风阀1开度", Length = 3)]
        public string jz2hff1kd { get; set; }
        /// <summary>
        /// Desc:机组2回风阀2开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2回风阀2开度", Length = 3)]
        public string jz2hff2kd { get; set; }



        //=154================================================================================================================
        ///   中间有预留

        ///   =162================================================================================================================





        //=166.167-6================================================================================================================

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
        /// Desc:机组1空气净化器运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1空气净化器运行", Length = 1)]
        public string jz1kqjhqyx { get; set; }

        /// <summary>
        /// Desc:机组2空气净化器运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2空气净化器运行", Length = 1)]
        public string jz2kqjhqyx { get; set; }
        //=166.167-6================================================================================================================


        /// <summary>
        /// Desc:机组2制冷系统2低压开关压力故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2低压开关压力故障", Length = 1)]
        public string jz2zlxt2dykgylgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1低压开关压力故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1低压开关压力故障", Length = 1)]
        public string jz2zlxt1dykgylgz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2低压开关压力故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2低压开关压力故障", Length = 1)]
        public string jz1zlxt2dykgylgz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1低压开关压力故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1低压开关压力故障", Length = 1)]
        public string jz1zlxt1dykgylgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统2低压传感器故障175-1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2低压传感器故障", Length = 1)]
        public string jz2zlxt2dycgqgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1低压传感器故障", Length = 1)]
        public string jz2zlxt1dycgqgz { get; set; }

        //===169-78========================

        /// <summary>
        /// Desc:机组1制冷系统2低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2低压传感器故障", Length = 1)]
        public string jz1zlxt2dycgqgz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1低压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1低压传感器故障", Length = 1)]
        public string jz1zlxt1dycgqgz { get; set; }


        //==169-78========================
        ////    ======170-2.3======================================
        /// <summary>
        /// Desc:机组1空气净化器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1空气净化器故障", Length = 1)]
        public string jz1kqjhqgz { get; set; }

        /// <summary>
        /// Desc:机组1压差传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压差传感器故障", Length = 1)]
        public string jz1yccgqgz { get; set; }

        /// <summary>
        /// Desc:机组2空气净化器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2空气净化器故障", Length = 1)]
        public string jz2kqjhqgz { get; set; }

        /// <summary>
        /// Desc:机组2压差传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压差传器故障", Length = 1)]
        public string jz2ygcgqgz { get; set; }
        ////  ============================================


        /// <summary>
        /// Desc:机组2制冷系统2高压开关压力故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2高压开关压力故障", Length = 1)]
        public string jz2zlxt2gykgylgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1高压开关压力故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1高压开关压力故障", Length = 1)]
        public string jz2zlxt1gykgylgz { get; set; }




        //   =====169=================================================================================================



        /// <summary>
        /// Desc:机组1制冷系统2高压开关压力故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2高压开关压力故障", Length = 1)]
        public string jz1zlxt2gykgylgz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1高压开关压力故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1高压开关压力故障", Length = 1)]
        public string jz1zlxt1gykgylgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统2高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2高压传感器故障", Length = 1)]
        public string jz2zlxt2gycgqgz { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1高压传感器故障", Length = 1)]
        public string jz2zlxt1gycgqgz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2高压传感器故障", Length = 1)]
        public string jz1zlxt2gycgqgz { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1高压传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1高压传感器故障", Length = 1)]
        public string jz1zlxt1gycgqgz { get; set; }

        // = 169 =====================================================

        /// <summary>
        /// Desc:机组2回送风温度传感器1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2回送风温度传感器1故障", Length = 1)]
        public string jz2hsfwdcgq1gz { get; set; }

        /// <summary>
        /// Desc:机组2送回风温度传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2送回风温度传感器故障", Length = 1)]
        public string jz2shfwdcgqgz { get; set; }

        /// <summary>
        /// Desc:机组1回送风温度传感器1故障170-4
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回送风温度传感器1故障", Length = 1)]
        public string jz1hsfwdcgq1gz { get; set; }

        /// <summary>
        /// Desc:机组1送回风温度传感器故障170-5
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1送回风温度传感器故障", Length = 1)]
        public string jz1shfwdcgqgz { get; set; }

        /// <summary>
        /// Desc:机组1紫外线灯2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1紫外线灯2故障", Length = 1)]
        public string jz1zwxd2gz { get; set; }

        /// <summary>
        /// Desc:机组1紫外线灯1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1紫外线灯1故障", Length = 1)]
        public string jz1zwxd1gz { get; set; }
        /// <summary>
        /// Desc:机组2紫外线灯2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2紫外线灯2故障", Length = 1)]
        public string jz2zwxd2gz { get; set; }

        /// <summary>
        /// Desc:机组2紫外线灯1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2紫外线灯1故障", Length = 1)]
        public string jz2zwxd1gz { get; set; }



        // ===171==================================
        /// <summary>
        /// Desc:机组1吸气温度传感器2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1吸气温度传感器2故障", Length = 1)]
        public string jz1xqwdcgq2gz { get; set; }
        /// <summary>
        /// Desc:机组1吸气温度传感器1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1吸气温度传感器1故障", Length = 1)]
        public string jz1xqwdcgq1gz { get; set; }

        /// <summary>
        /// Desc:机组2吸气温度传感器2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2吸气温度传感器2故障", Length = 1)]
        public string jz2xqwdcgq2gz { get; set; }

        /// <summary>
        /// Desc:机组2吸气温度传感器1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2吸气温度传感器1故障", Length = 1)]
        public string jz2xqwdcgq1gz { get; set; }

        /// <summary>
        /// Desc:机组1排气温度传感器2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1排气温度传感器2故障", Length = 1)]
        public string jz1pqwdcgq2gz { get; set; }

        /// <summary>
        /// Desc:机组1排气温度传感器1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1排气温度传感器1故障", Length = 1)]
        public string jz1pqwdcgq1gz { get; set; }


        /// <summary>
        /// Desc:机组2排气温度传感器2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2排气温度传感器2故障", Length = 1)]
        public string jz2pqwdcgq2gz { get; set; }

        /// <summary>
        /// Desc:机组2排气温度传感器1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2排气温度传感器1故障", Length = 1)]
        public string jz2pqwdcgq1gz { get; set; }



        /// <summary>
        /// Desc:机组1冷凝温度传感器2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝温度传感器2故障", Length = 1)]
        public string jz1lnwdcgq2gz { get; set; }

        /// <summary>
        /// Desc:机组1冷凝温度传感器1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝温度传感器1故障", Length = 1)]
        public string jz1lnwdcgq1gz { get; set; }


        /// <summary>
        /// Desc:机组2冷凝温度传感器2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝温度传感器2故障", Length = 1)]
        public string jz2lnwdcgq2gz { get; set; }

        /// <summary>
        /// Desc:机组2冷凝温度传感器1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝温度传感器1故障", Length = 1)]
        public string jz2lnwdcgq1gz { get; set; }


        // ==173===========================================

        /// <summary>
        /// Desc:机组1变频器2硬线故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1变频器2硬线故障", Length = 1)]
        public string jz1bpq2yxgz { get; set; }

        /// <summary>
        /// Desc:机组1变频器1硬线故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1变频器1硬线故障", Length = 1)]
        public string jz1bpq1yxgz { get; set; }


        /// <summary>
        /// Desc:机组2变频器2硬线故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2变频器2硬线故障", Length = 1)]
        public string jz2bpq2yxgz { get; set; }

        /// <summary>
        /// Desc:机组2变频器1硬线故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2变频器1硬线故障", Length = 1)]
        public string jz2bpq1yxgz { get; set; }


        /// <summary>
        /// Desc:机组1紧急通风接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1紧急通风接触器故障", Length = 1)]
        public string jz1jjtfjcqgz { get; set; }

        /// <summary>
        /// Desc:机组2紧急通风接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2紧急通风接触器故障", Length = 1)]
        public string jz2jjtfjcqgz { get; set; }


        /// <summary>
        /// Desc:机组1正常通风接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1正常通风接触器故障", Length = 1)]
        public string jz1zctfjcqgz { get; set; }

        /// <summary>
        /// Desc:机组2正常通风接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2正常通风接触器故障", Length = 1)]
        public string jz2zctfjcqgz { get; set; }



        /// <summary>
        /// Desc:机组1冷凝风机2接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2接触器故障", Length = 1)]
        public string jz1lnfj2jcqgz { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机1接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1接触器故障", Length = 1)]
        public string jz1lnfj1jcqgz { get; set; }




        /// <summary>
        /// Desc:机组2冷凝风机2接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机2接触器故障", Length = 1)]
        public string jz2lnfj2jcqgz { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机1接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机1接触器故障", Length = 1)]
        public string jz2lnfj1jcqgz { get; set; }



        /// <summary>
        /// Desc:机组1压缩机2接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2接触器故障", Length = 1)]
        public string jz1ysj2cqgz { get; set; }

        /// <summary>
        /// Desc:机组1压缩机1接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1接触器故障", Length = 1)]
        public string jz1ysj1cqgz { get; set; }



        /// <summary>
        /// Desc:机组2压缩机2接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2接触器故障", Length = 1)]
        public string jz2ysj2cqgz { get; set; }

        /// <summary>
        /// Desc:机组2压缩机1接触器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1接触器故障", Length = 1)]
        public string jz2ysj1cqgz { get; set; }




        /// <summary>
        /// Desc:机组1电子膨胀阀2通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1电子膨胀阀2通讯故障", Length = 1)]
        public string jz1dzpzf2txgz { get; set; }

        /// <summary>
        /// Desc:机组1电子膨胀阀2通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1电子膨胀阀1通讯故障", Length = 1)]
        public string jz1dzpzf1txgz { get; set; }



        /// <summary>
        /// Desc:机组2电子膨胀阀2通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2电子膨胀阀2通讯故障", Length = 1)]
        public string jz2dzpzf2txgz { get; set; }

        /// <summary>
        /// Desc:机组2电子膨胀阀1通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2电子膨胀阀1通讯故障", Length = 1)]
        public string jz2dzpzf1txgz { get; set; }


        /// <summary>
        /// Desc:机组1采集模块模块通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1采集模块模块通讯故障", Length = 1)]
        public string jz1cjmktxgz { get; set; }

        /// <summary>
        /// Desc:机组1采集模块模块通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2采集模块模块通讯故障", Length = 1)]
        public string jz2cjmktxgz { get; set; }


        /// <summary>
        /// Desc:机组1空气质量模块通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1空气质量模块通讯故障", Length = 1)]
        public string jz1kqzlmktxgz { get; set; }

        /// <summary>
        /// Desc:机组2空气质量模块通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2空气质量模块通讯故障", Length = 1)]
        public string jz2kqzlmktxgz { get; set; }


        /// <summary>
        /// Desc:机组1变频器2通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1变频器2通讯故障", Length = 1)]
        public string jz1bpq2txgz { get; set; }

        /// <summary>
        /// Desc:机组1变频器1通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1变频器1通讯故障", Length = 1)]
        public string jz1bpq1txgz { get; set; }


        /// <summary>
        /// Desc:机组2变频器2通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2变频器2通讯故障", Length = 1)]
        public string jz2bpq2txgz { get; set; }

        /// <summary>
        /// Desc:机组1变频器1通讯故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2变频器1通讯故障", Length = 1)]
        public string jz2bpq1txgz { get; set; }


        /// <summary>
        /// Desc:机组1三相检测故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1三相检测故障", Length = 1)]
        public string jz1sxjcgz { get; set; }

        /// <summary>
        /// Desc:机组2三相检测故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2三相检测故障", Length = 1)]
        public string jz2sxjcgz { get; set; }


        /// <summary>
        /// Desc:电流采集模块通讯故障 181-1
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "电流采集模块通讯故障181-1", Length = 2)]
        public string dlcjmktxgz { get; set; }




        /// <summary>
        /// Desc:机组2压缩机1排气温度保护故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1排气温度保护故障", Length = 1)]
        public string jz2ysj1pqwdbhgz { get; set; }

        /// <summary>
        /// Desc:机组1压缩机1排气温度保护故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1排气温度保护故障", Length = 1)]
        public string jz1ysj1pqwdbhgz { get; set; }


        /// <summary>
        /// Desc:机组2回送风温度传感器2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2回送风温度传感器2故障", Length = 1)]
        public string jz2hsfwdcgq2gz { get; set; }


        /// <summary>
        /// Desc:机组2新风温度传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2新风温度传感器故障", Length = 1)]
        public string jz2xfwdcgqgz { get; set; }

        /// <summary>
        /// Desc:机组1回送风温度传感器2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回送风温度传感器2故障", Length = 1)]
        public string jz1hsfwdcgq2gz { get; set; }

        /// <summary>
        /// Desc:机组1新风温度传感器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1新风温度传感器故障", Length = 1)]
        public string jz1xfwdcgqgz { get; set; }


        /// <summary>
        /// Desc:机组2压缩机2排气温度保护故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2排气温度保护故障", Length = 1)]
        public string jz2ysj2pqwdbhgz { get; set; }

        /// <summary>
        /// Desc:机组1压缩机2排气温度保护故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2排气温度保护故障", Length = 1)]
        public string jz1ysj2pqwdbhgz { get; set; }

        /// <summary>
        /// Desc:机组2新风电动风阀2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2新风电动风阀2故障", Length = 1)]
        public string jz2xfddff2gz { get; set; }


        /// <summary>
        /// Desc:机组1回风电动风阀1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回风电动风阀1故障", Length = 1)]
        public string jz1hfddff1gz { get; set; }

        /// <summary>
        /// Desc:机组1回风电动风阀2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回风电动风阀2故障", Length = 1)]
        public string jz1hfddff2gz { get; set; }

        /// <summary>
        /// Desc:机组1新风电动风阀2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1新风电动风阀2故障", Length = 1)]
        public string jz1xfddff2gz { get; set; }


        /// <summary>
        /// Desc:机组2回风电动风阀1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2回风电动风阀1故障", Length = 1)]
        public string jz2hfddff1gz { get; set; }

        /// <summary>
        /// Desc:机组2新风电动风阀1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2新风电动风阀1故障", Length = 1)]
        public string jz2xfddff1gz { get; set; }




        /// <summary>
        /// Desc:机组1新风电动风阀1故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1新风电动风阀1故障", Length = 1)]
        public string jz1xfddff1gz { get; set; }


        /// <summary>
        /// Desc:机组2主断路器断开
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2主断路器断开", Length = 1)]
        public string jz2zdlqdk { get; set; }

        /// <summary>
        /// Desc:机组1主断路器断开
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1主断路器断开", Length = 1)]
        public string jz1zdlqdk { get; set; }



        /// <summary>
        /// Desc:机组2回风电动风阀2故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2回风电动风阀2故障", Length = 1)]
        public string jz2hfddff2gz { get; set; }



        /// <summary>
        /// Desc:空调紧急逆变器故障
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "空调紧急逆变器故障", Length = 1)]
        public string ktjjnbqgz { get; set; }




        //======================================================================
        /// <summary>
        /// Desc:紧急通风逆变器运行信号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "紧急通风逆变器运行信号", Length = 2)]
        public string jjtfnbqyxxh { get; set; }


        /// <summary>
        /// Desc:机组2制冷系统2旁通电磁阀运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2旁通电磁阀运行", Length = 2)]
        public string jz2zlxt2ptdcf { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1旁通电磁阀运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1旁通电磁阀运行", Length = 2)]
        public string jz2zlxt1ptdcf { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2旁通电磁阀运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2旁通电磁阀运行", Length = 2)]
        public string jz1zlxt2ptdcf { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1旁通电磁阀运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1旁通电磁阀运行", Length = 2)]
        public string jz1zlxt1ptdcf { get; set; }

        //====168=================================


        /// <summary>
        /// Desc:机组2制冷系统2泄漏预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统2泄漏预警", Length = 1)]
        public string jz2zlxt2xlyj { get; set; }

        /// <summary>
        /// Desc:机组2制冷系统1泄漏预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2制冷系统1泄漏预警", Length = 1)]
        public string jz2zlxt1xlyj { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统2泄漏预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统2泄漏预警", Length = 1)]
        public string jz1zlxt2xlyj { get; set; }

        /// <summary>
        /// Desc:机组1制冷系统1泄漏预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1制冷系统1泄漏预警", Length = 1)]
        public string jz1zlxt1xlyj { get; set; }


        /// <summary>
        /// Desc:机组2滤网脏堵预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2滤网脏堵预警", Length = 1)]
        public string jz2lwzdyj { get; set; }

        /// <summary>
        /// Desc:机组1滤网脏堵预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1滤网脏堵预警", Length = 1)]
        public string jz1lwzdyj { get; set; }



        /// <summary>
        /// Desc:机组2冷凝风机2故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机2故障预警", Length = 1)]
        public string jz2lnfj2gzyj { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机1故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机1故障预警", Length = 1)]
        public string jz2lnfj1gzyj { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机2故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2故障预警", Length = 1)]
        public string jz1lnfj2gzyj { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机1故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1故障预警", Length = 1)]
        public string jz1lnfj1gzyj { get; set; }

        /// <summary>
        /// Desc:机组2蒸发风机2故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机2故障预警", Length = 1)]
        public string jz2zffj2gzyj { get; set; }

        /// <summary>
        /// Desc:机组2蒸发风机1故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机1故障预警", Length = 1)]
        public string jz2zffj1gzyj { get; set; }

        /// <summary>
        /// Desc:机组1蒸发风机2故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机2故障预警", Length = 1)]
        public string jz1zffj2gzyj { get; set; }

        /// <summary>
        /// Desc:机组1蒸发风机1故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机1故障预警", Length = 1)]
        public string jz1zffj1gzyj { get; set; }



        /// <summary>
        /// Desc:机组2压缩机2寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2寿命预警", Length = 1)]
        public string jz2ysj2smyj { get; set; }

        /// <summary>
        /// Desc:机组2压缩机1寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1寿命预警", Length = 1)]
        public string jz2ysj1smyj { get; set; }

        /// <summary>
        /// Desc:机组1压缩机2寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2寿命预警", Length = 1)]
        public string jz1ysj2smyj { get; set; }

        /// <summary>
        /// Desc:机组1压缩机1寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1寿命预警", Length = 1)]
        public string jz1ysj1smyj { get; set; }

        /// <summary>
        /// Desc:机组2压缩机2故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2故障预警", Length = 1)]
        public string jz2ysj2gzyj { get; set; }

        /// <summary>
        /// Desc:机组2压缩机1故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1故障预警", Length = 1)]
        public string jz2ysj1gzyj { get; set; }

        /// <summary>
        /// Desc:机组1压缩机2故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2故障预警", Length = 1)]
        public string jz1ysj2gzyj { get; set; }

        /// <summary>
        /// Desc:机组1压缩机1故障预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1故障预警", Length = 1)]
        public string jz1ysj1gzyj { get; set; }



        /// <summary>
        /// Desc:机组1冷凝风机1寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1寿命预警", Length = 1)]
        public string jz1lnfj1smyj { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机2寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2寿命预警", Length = 1)]
        public string jz1lnfj2smyj { get; set; }



        /// <summary>
        /// Desc:机组2冷凝风机1寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机1寿命预警", Length = 1)]
        public string jz2lnfj1smyj { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机2寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机2寿命预警", Length = 1)]
        public string jz2lnfj2smyj { get; set; }



        /// <summary>
        /// Desc:机组1蒸发风机1寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机1寿命预警", Length = 1)]
        public string jz1zffj1smyj { get; set; }

        /// <summary>
        /// Desc:机组1蒸发风机2寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机2寿命预警", Length = 1)]
        public string jz1zffj2smyj { get; set; }


        /// <summary>
        /// Desc:机组2蒸发风机1寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机1寿命预警", Length = 1)]
        public string jz2zffj1smyj { get; set; }

        /// <summary>
        /// Desc:机组2蒸发风机2寿命预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机2寿命预警", Length = 1)]
        public string jz2zffj2smyj { get; set; }


        /// <summary>
        /// Desc:车内空气质量预警
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "车内空气质量预警", Length = 1)]
        public string cnkqzlyj { get; set; }



        /// <summary>
        /// Desc:机组1冷凝风机2过载
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2过载", Length = 1)]
        public string jz1lnfj2gz { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机1过载
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1过载", Length = 1)]
        public string jz1lnfj1gz { get; set; }

        /// <summary>
        /// Desc:机组1蒸发风机2过载
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机2过载", Length = 1)]
        public string jz1zffj2gz { get; set; }

        /// <summary>
        /// Desc:机组1蒸发风机1过载
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1蒸发风机1过载", Length = 1)]
        public string jz1zffj1gz { get; set; }


        /// <summary>
        /// Desc:机组2冷凝风机2过载
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机2过载", Length = 1)]
        public string jz2lnfj2gz { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机1过载
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机1过载", Length = 1)]
        public string jz2lnfj1gz { get; set; }

        /// <summary>
        /// Desc:机组2蒸发风机2过载
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机2过载", Length = 1)]
        public string jz2zffj2gz { get; set; }

        /// <summary>
        /// Desc:机组2蒸发风机1过载
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2蒸发风机1过载", Length = 1)]
        public string jz2zffj1gz { get; set; }



        //=166 4==================================================>>
        /// <summary>
        /// Desc:机组1压缩机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2运行", Length = 1)]
        public string jz1ysj2yx { get; set; }

        /// <summary>
        /// Desc:机组1压缩机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1运行", Length = 1)]
        public string jz1ysj1yx { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2运行", Length = 1)]
        public string jz1lnfj2yx { get; set; }

        /// <summary>
        /// Desc:机组1冷凝风机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1运行", Length = 1)]
        public string jz1lnfj1yx { get; set; }

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



        /// <summary>
        /// Desc:机组2压缩机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机2运行", Length = 1)]
        public string jz2ysj2yx { get; set; }

        /// <summary>
        /// Desc:机组2压缩机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2压缩机1运行", Length = 1)]
        public string jz2ysj1yx { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机2运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机2运行", Length = 1)]
        public string jz2lnfj2yx { get; set; }

        /// <summary>
        /// Desc:机组2冷凝风机1运行
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2冷凝风机1运行", Length = 1)]
        public string jz2lnfj1yx { get; set; }

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

        //==166-167=================================================<<



        //==162-165=================================================================================》》
        /// <summary>
        /// Desc:机组1控制模式 Bit7： 0:集控；1:本控  Bit6-Bit0： 0x00:UIC；0x01:除菌；0x02:除湿；0x03:关闭预冷；0x04:测试；0x05:通风；0x06:火灾；0x7F:无指令。最高位为当前控制方式，其余位代表空调当前执行的命令。
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1控制模式 Bit7： 0:集控；1:本控  Bit6-Bit0： 0x00:UIC；0x01:除菌；0x02:除湿；0x03:关闭预冷；0x04:测试；0x05:通风；0x06:火灾；0x7F:无指令。 最高位为当前控制方式，其余位代表空调当前执行的命令。", Length = 4)]
        public string jz1kzms { get; set; }

        /// <summary>
        /// Desc:机组1工作模式 机组1运行模式0x00:停机模式；0x01:除菌模式；0x02:减载模式；0x03:预冷模式；0x04:紧急通风模式；0x05:除湿模式；0x06:制冷模式；0x07:火灾模式；0x08:通风模式；0xFF:无效。
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1工作模式 机组1运行模式0x00:停机模式；0x01:除菌模式；0x02:减载模式；0x03:预冷模式；0x04:紧急通风模式；0x05:除湿模式；0x06:制冷模式；0x07:火灾模式；0x08:通风模式；0xFF:无效。", Length = 4)]
        public string jz1gzms { get; set; }

        /// <summary>
        /// Desc:机组2控制模式Bit7： 0:集控；1:本控 Bit6-Bit0： 0x00:UIC；0x01:除菌；0x02:除湿；0x03:关闭预冷；0x04:测试；0x05:通风；0x06:火灾；0x7F:无指令。最高位为当前控制方式，其余位代表空调当前执行的命令
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2控制模式Bit7： 0:集控；1:本控 Bit6-Bit0： 0x00:UIC；0x01:除菌；0x02:除湿；0x03:关闭预冷；0x04:测试；0x05:通风；0x06:火灾；0x7F:无指令。最高位为当前控制方式，其余位代表空调当前执行的命令", Length = 4)]
        public string jz2kzms { get; set; }

        /// <summary>
        /// Desc:机组2工作模式 机组2运行模式0x00:停机模式；0x01:除菌模式；0x02:减载模式；0x03:预冷模式；0x04:紧急通风模式；0x05:除湿模式；0x06:制冷模式；0x07:火灾模式；0x08:通风模式；0xFF:无效。
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组2工作模式  机组2运行模式0x00:停机模式；0x01:除菌模式；0x02:减载模式；0x03:预冷模式；0x04:紧急通风模式；0x05:除湿模式；0x06:制冷模式；0x07:火灾模式；0x08:通风模式；0xFF:无效。", Length = 4)]
        public string jz2gzms { get; set; }
        //==162-165=================================================================================《《


        /// <summary>
        /// Desc:TCMS:生命信号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "TCMS:生命信号", Length = 5)]
        public string TCMSsmxh { get; set; }

        /// <summary>
        /// Desc:TCMS:本车载荷
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "TCMS:本车载荷", Length = 6)]
        public string TCMSbczh { get; set; }

        /// <summary>
        /// Desc:TCMS:列车速度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "TCMS:列车速度", Length = 6)]
        public string TCMSlcsd { get; set; }



        /// <summary>
        /// Desc:TCMS:测试模式
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:测试模式", Length = 1)]
        public string TCMScwshims { get; set; }

        /// <summary>
        /// Desc:TCMS:除湿模式
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:除湿模式", Length = 1)]
        public string TCMSchushims { get; set; }

        /// <summary>
        /// Desc:TCMS:通风模式
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:通风模式", Length = 1)]
        public string TCMStfms { get; set; }

        /// <summary>
        /// Desc:TCMS :UIC模式
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS :UIC模式", Length = 1)]
        public string TCMSuicms { get; set; }

        /// <summary>
        /// Desc:TCMS:除菌模式
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:除菌模式", Length = 1)]
        public string TCMScjms { get; set; }

        /// <summary>
        /// Desc:TCMS:火灾模式
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:火灾模式", Length = 1)]
        public string TCMShzms { get; set; }

        /// <summary>
        /// Desc:TCMS:关闭预冷
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:关闭预冷", Length = 1)]
        public string TCMSgbyl { get; set; }

        /// <summary>
        /// Desc:TCMS:空调减载
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:空调减载", Length = 1)]
        public string TCMSktjc { get; set; }

        /// <summary>
        /// Desc:TCMS :允许空调机组2压缩机启动
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS :允许空调机组2压缩机启动", Length = 1)]
        public string TCMSyxktjz2ysjqd { get; set; }

        /// <summary>
        /// Desc:TCMS :允许空调机组1压缩机启动
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS :允许空调机组1压缩机启动", Length = 1)]
        public string TCMSyxktjz1ysjqd { get; set; }

        /// <summary>
        /// Desc:TCMS:+2K 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:+2K", Length = 1)]
        public string TCMSz2k { get; set; }

        /// <summary>
        /// Desc:TCMS:+1K 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:+1K", Length = 1)]
        public string TCMSz1k { get; set; }

        /// <summary>
        /// Desc:TCMS: -1K 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS: -1K", Length = 1)]
        public string TCMSf1k { get; set; }

        /// <summary>
        /// Desc:TCMS: -2K 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS: -2K", Length = 1)]
        public string TCMSf2k { get; set; }

        /// <summary>
        /// Desc:TCMS:空调关 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:空调关", Length = 1)]
        public string TCMSktoff { get; set; }

        /// <summary>
        /// Desc:TCMS:空调开 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:空调开", Length = 1)]
        public string TCMSkton { get; set; }

        /// <summary>
        /// Desc:TCMS:室内火灾 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS:室内火灾", Length = 1)]
        public string TCMSsnhz { get; set; }

        /// <summary>
        /// Desc:TCMS: 27℃ 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS: 27℃", Length = 1)]
        public string TCMS27d { get; set; }

        /// <summary>
        /// Desc:TCMS: 25℃ 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS: 25℃", Length = 1)]
        public string TCMS25d { get; set; }

        /// <summary>
        /// Desc:TCMS: 23℃ 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS: 23℃", Length = 1)]
        public string TCMS23d { get; set; }

        /// <summary>
        /// Desc:TCMS: 21℃ 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS: 21℃", Length = 1)]
        public string TCMS21d { get; set; }

        /// <summary>
        /// Desc:TCMS: 19℃ 
        /// Default: 
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnDescription = " TCMS: 19℃", Length = 1)]
        public string TCMS19d { get; set; }


        /// <summary>
        /// Desc:数据长度（大端10进制计算）
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "数据长度（大端10进制计算）", Length = 50)]
        public string sjcd { get; set; }



        /// <summary>
        /// Desc:线路ID 8
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "线路ID 8", Length = 2)]
        public string xlid { get; set; }

        ///// <summary>
        ///// Desc:编组ID
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[SugarColumn(ColumnDescription = "编组ID", Length = 4)]
        //public string bzid { get; set; }

        /// <summary>
        /// Desc:设备ID（1 or 2）
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "设备ID（1 or 2）", Length = 1)]
        public string sbid { get; set; }

        /// <summary>
        /// Desc:年月日时分秒.毫秒
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "年月日时分秒.毫秒", Length = 26)]
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
        public long? ybwid { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "创建时间")]
        public DateTime create_time { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "")]
        public long timestamp { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "kafka offset")]
        public long offset { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "partition")]
        public int partition { get; set; }

    }
}
