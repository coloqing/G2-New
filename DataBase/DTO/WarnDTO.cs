using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DTO
{
    public class WarnDTO
    {

        /// <summary>
        /// Desc:列车号和网络MVB协议中列车号保持一致
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "列车号和网络MVB协议中列车号保持一致", Length = 50)]
        public string lch { get; set; }

        /// <summary>
        /// Desc:车厢号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "车厢号")]
        public string cxh { get; set; }

        /// <summary>
        /// 车厢号别名(ABCCBA)
        /// </summary>
        [SugarColumn(ColumnDescription = "车厢号别名")]
        public string cxhName { get; set; }

        /// <summary>
        /// Desc:设备编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "设备编码", Length = 50)]
        public string device_code { get; set; }

        /// <summary>
        /// Desc:源系统主机ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "源系统主机ID")]
        public int yxtzjid { get; set; }

        /// <summary> 
        /// Desc:机组1目标温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1目标温度")]
        public double jz1mbwd { get; set; }

        /// <summary> 
        /// Desc:机组1客室温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1客室温度")]
        public double jz1kswd { get; set; }

        /// <summary> 
        /// Desc:机组1室外温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1室外温度")]
        public double jz1swwd { get; set; }

        /// <summary> 
        /// Desc:客室温度传感器1温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "客室温度传感器1温度")]
        public double jz1kswdcgq1wd { get; set; }

        /// <summary> 
        /// Desc:机组1送风传感器1温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1送风传感器1温度")]
        public double jz1sfcgq1wd { get; set; }

        /// <summary> 
        /// Desc:机组1送风传感器2温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1送风传感器2温度")]
        public double jz1sfcgq2wd { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1排气温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1排气温度")]
        public double jz1ysj1pqwd { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2排气温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2排气温度")]
        public double jz1ysj2pqwd { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1吸气温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1吸气温度")]
        public double jz1ysj1xqwd { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2吸气温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2吸气温度")]
        public double jz1ysj2xqwd { get; set; }

        /// <summary> 
        /// Desc:机组1空气质量检测模块温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1空气质量检测模块温度")]
        public double jz1kqzljcmkwd { get; set; }

        /// <summary> 
        /// Desc:机组1CO2浓度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1CO2浓度")]
        public int jz1co2nd { get; set; }

        /// <summary> 
        /// Desc:机组1PM2.5浓度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1PM2.5浓度")]
        public int jz1pm2d5nd { get; set; }

        /// <summary> 
        /// Desc:机组1TVOC浓度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1TVOC浓度")]
        public int jz1tvocnd { get; set; }

        /// <summary> 
        /// Desc:客室湿度值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1客室湿度值")]
        public int kssdz { get; set; }

        [SugarColumn(ColumnDescription = "机组1客室湿度值")]
        public int jz1kssdz { get; set; }


        /// <summary> 
        /// Desc:机组1新风阀1开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1新风阀1开度")]
        public int jz1xff1kd { get; set; }

        /// <summary> 
        /// Desc:机组1新风阀2开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1新风阀2开度")]
        public int jz1xff2kd { get; set; }

        /// <summary> 
        /// Desc:机组1回风阀1开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回风阀1开度")]
        public int jz1hff1kd { get; set; }

        /// <summary> 
        /// Desc:机组1回风阀2开度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1回风阀2开度")]
        public int jz1hff2kd { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1高压压力
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1高压压力")]
        public int jz1ysj1gyyl { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1低压压力
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1低压压力")]
        public int jz1ysj1dyyl { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2高压压力
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2高压压力")]
        public int jz1ysj2gyyl { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2低压压力
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2低压压力")]
        public int jz1ysj2dyyl { get; set; }

        /// <summary> 
        /// Desc:机组1滤网压力值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1滤网压力值")]
        public int jz1lwylz { get; set; }

        /// <summary> 
        /// Desc:机组1通风机1 U相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机1 U相电流值")]
        public double jz1tfj1uxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1通风机1 V相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机1 V相电流值")]
        public double jz1tfj1vxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1通风机1 W相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机1 W相电流值")]
        public double jz1tfj1wxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1通风机2 U相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机2 U相电流值")]
        public double jz1tfj2uxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1通风机2 V相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机2 V相电流值")]
        public double jz1tfj2vxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1通风机2 W相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机2 W相电流值")]
        public double jz1tfj2wxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机1 U相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1 U相电流值")]
        public double jz1lnfj1uxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机1 V相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1 V相电流值")]
        public double jz1lnfj1vxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机1 W相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1 W相电流值")]
        public double jz1lnfj1wxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机2 U相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2 U相电流值")]
        public double jz1lnfj2uxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机2 V相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2 V相电流值")]
        public double jz1lnfj2vxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机2 W相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2 W相电流值")]
        public double jz1lnfj2wxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1 U相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1 U相电流值")]
        public double jz1ysj1uxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1 V相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1 V相电流值")]
        public double jz1ysj1vxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1 W相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1 W相电流值")]
        public double jz1ysj1wxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2 U相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2 U相电流值")]
        public double jz1ysj2uxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2 V相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2 V相电流值")]
        public double jz1ysj2vxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2 W相电流值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2 W相电流值")]
        public double jz1ysj2wxdlz { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1频率
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1频率")]
        public int jz1ysj1pl { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2频率
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2频率")]
        public int jz1ysj2pl { get; set; }

        /// <summary> 
        /// Desc:机组1变频器1功率
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1变频器1功率")]
        public double jz1bpq1gl { get; set; }

        /// <summary> 
        /// Desc:机组1变频器2功率
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1变频器2功率")]
        public double jz1bpq2gl { get; set; }

        /// <summary> 
        /// Desc:机组1变频器1输出电压
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1变频器1输出电压")]
        public double jz1bpq1scdy { get; set; }

        /// <summary> 
        /// Desc:机组1变频器2输出电压
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1变频器2输出电压")]
        public double jz1bpq2scdy { get; set; }

        /// <summary> 
        /// Desc:机组1空调能耗
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1空调能耗")]
        public int jz1ktnh { get; set; }

        /// <summary> 
        /// Desc:机机组1冷凝风机1累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1累计工作时间")]
        public int jz1lnfj1ljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机2累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2累计工作时间")]
        public int jz1lnfj2ljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机1轴承累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1轴承累计工作时间")]
        public int jz1lnfj1zcljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机2轴承累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2轴承累计工作时间")]
        public int jz1lnfj2zcljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1通风机1累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机1累计工作时间")]
        public int jz1tfj1ljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1通风机2累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机2累计工作时间")]
        public int jz1tfj2ljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1通风机1轴承累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机1轴承累计工作时间")]
        public int jz1tfj1zcljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1通风机2轴承累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机2轴承累计工作时间")]
        public int jz1tfj2zcljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1累计工作时间")]
        public int jz1ysj1ljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2累计工作时间")]
        public int jz1ysj2ljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1空气净化器累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1空气净化器累计工作时间")]
        public int jz1kqjhqljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1紫外线灯累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1紫外线灯累计工作时间")]
        public int jz1zwxdljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1空气净化器灯管累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1空气净化器灯管累计工作时间")]
        public int jz1kqjhqdgljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1紫外线灯灯管累计工作时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1紫外线灯灯管累计工作时间")]
        public int jz1zwxddgljgzsj { get; set; }

        /// <summary> 
        /// Desc:机组1通风机紧急通风接触器动作次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机紧急通风接触器动作次数")]
        public int jz1tfjjjtfjcqdzcs { get; set; }

        /// <summary> 
        /// Desc:机组1通风机接触器动作次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机接触器动作次数")]
        public int jz1tfjjcqdzcs { get; set; }

        /// <summary> 
        /// Desc:机组1通风机2接触器动作次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机2接触器动作次数")]
        public int jz1tfj2jcqdzcs { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机接触器动作次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机接触器动作次数")]
        public int jz1lnfjjcqdzcs { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机1接触器动作次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机1接触器动作次数")]
        public int jz1ysj1jcqdzcs { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机2接触器动作次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机2接触器动作次数")]
        public int jz1ysj2jcqdzcs { get; set; }

        /// <summary> 
        /// Desc:通风模式
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "通风模式")]
        public int tfms { get; set; }

        /// <summary> 
        /// Desc:机组1紫外线灯状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1紫外线灯状态")]
        public int jz1zwxdzt { get; set; }

        /// <summary> 
        /// Desc:机组1空气净化器状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1空气净化器状态")]
        public int jz1kqjhqzt { get; set; }

        /// <summary> 
        /// Desc:机组1通风机1状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机1状态")]
        public int jz1tfj1zt { get; set; }

        /// <summary> 
        /// Desc:机组1通风机2状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1通风机2状态")]
        public int jz1tfj2zt { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机1状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机1状态")]
        public int jz1lnfj1zt { get; set; }

        /// <summary> 
        /// Desc:机组1冷凝风机2状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1冷凝风机2状态")]
        public int jz1lnfj2zt { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机机1状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机机1状态")]
        public int jz1ysj1zt { get; set; }

        /// <summary> 
        /// Desc:机组1压缩机机2状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "机组1压缩机机2状态")]
        public int jz1ysjj2zt { get; set; }

        /// <summary> 
        /// Desc:紧急通风状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "紧急通风状态")]
        public int jjtfzt { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "创建时间")]
        public DateTime create_time { get; set; }
    }
}
