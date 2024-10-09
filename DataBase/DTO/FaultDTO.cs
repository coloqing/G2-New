using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DTO
{
    public class FaultDTO
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
        /// Desc:设备编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(ColumnDescription = "设备编码", Length = 50)]
        public string device_code { get; set; }


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
        public string rq { get; set; }
        public DateTime rqDateTime { get; set; }
    }
}
