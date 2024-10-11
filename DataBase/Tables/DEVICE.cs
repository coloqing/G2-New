using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    /// 设备表
    ///</summary>
    [SugarTable("DEVICE")]
    public partial class DEVICE
    {
           public DEVICE(){

            this.create_time =DateTime.Now;
            this.zxtime =Convert.ToInt32("0");
            this.main_line =Convert.ToInt32("-1");

           }
           /// <summary>
           /// Desc:主键ID  设备表
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnDescription="主键ID  设备表")]
           public int id {get;set;}

           /// <summary>
           /// Desc:设备编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="设备编码",Length=255)]
           public string device_id {get;set;}

           /// <summary>
           /// Desc:列车表ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="列车表ID")]
           public int? lcid {get;set;}

           /// <summary>
           /// Desc:列车号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="列车号",Length=255)]
           public string lch {get;set;}

           /// <summary>
           /// Desc:车厢表ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车厢表ID")]
           public int? cxid {get;set;}

           /// <summary>
           /// Desc:车厢号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车厢号",Length=255)]
           public string cxh {get;set;}

           /// <summary>
           /// Desc:机组数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组数量")]
           public int? jzsl {get;set;}

           /// <summary>
           /// Desc:设备状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="设备状态",Length=255)]
           public string state {get;set;}

           /// <summary>
           /// Desc:创建用户id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建用户id",Length=50)]
           public string create_userid {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建时间")]
           public DateTime? create_time {get;set;}

           /// <summary>
           /// Desc:更新用户id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="更新用户id",Length=50)]
           public string update_userid {get;set;}

           /// <summary>
           /// Desc:更新时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="更新时间")]
           public DateTime? update_time {get;set;}

           /// <summary>
           /// Desc:上线时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="上线时间")]
           public DateTime? sxtime {get;set;}

           /// <summary>
           /// Desc:空调类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="空调类型",Length=255)]
           public string kttype {get;set;}

           /// <summary>
           /// Desc:产品编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="产品编号",Length=255)]
           public string cpbh {get;set;}

           /// <summary>
           /// Desc:产品品类
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="产品品类",Length=255)]
           public string cppl {get;set;}

           /// <summary>
           /// Desc:维保期限
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="维保期限",Length=255)]
           public string wbtime {get;set;}

           /// <summary>
           /// Desc:运营单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="运营单位",Length=255)]
           public string yydw {get;set;}

           /// <summary>
           /// Desc:生产日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="生产日期")]
           public DateTime? sctime {get;set;}

           /// <summary>
           /// Desc:安装日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="安装日期")]
           public DateTime? aztime {get;set;}

           /// <summary>
           /// Desc:在线时长(秒)
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="在线时长(秒)")]
           public int? zxtime {get;set;}

           /// <summary>
           /// Desc:最后在线时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="最后在线时间")]
           public DateTime? jxsjsj {get;set;}

           /// <summary>
           /// Desc:废排风机1运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="废排风机1运行",Length=10)]
           public string fpfj1yx {get;set;}

           /// <summary>
           /// Desc:机组1压缩机2运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1压缩机2运行",Length=10)]
           public string jz1ysj2yx {get;set;}

           /// <summary>
           /// Desc:机组1压缩机1运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1压缩机1运行",Length=10)]
           public string jz1ysj1yx {get;set;}

           /// <summary>
           /// Desc:机组1冷凝风机2运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1冷凝风机2运行",Length=10)]
           public string jz1lnfj2yx {get;set;}

           /// <summary>
           /// Desc:机组1冷凝风机1运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1冷凝风机1运行",Length=10)]
           public string jz1lnfj1yx {get;set;}

           /// <summary>
           /// Desc:机组1送风机2运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1送风机2运行",Length=10)]
           public string jz1sfj2yx {get;set;}

           /// <summary>
           /// Desc:机组1送风机1运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组1送风机1运行",Length=10)]
           public string jz1sfj1yx {get;set;}

           /// <summary>
           /// Desc:废排风机2运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="废排风机2运行",Length=10)]
           public string fpfj2yx {get;set;}

           /// <summary>
           /// Desc:机组2压缩机2运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2压缩机2运行",Length=10)]
           public string jz2ysj2yx {get;set;}

           /// <summary>
           /// Desc:机组2压缩机1运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2压缩机1运行",Length=10)]
           public string jz2ysj1yx {get;set;}

           /// <summary>
           /// Desc:机组2冷凝风机2运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2冷凝风机2运行",Length=10)]
           public string jz2lnfj2yx {get;set;}

           /// <summary>
           /// Desc:机组2冷凝风机1运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2冷凝风机1运行",Length=10)]
           public string jz2lnfj1yx {get;set;}

           /// <summary>
           /// Desc:机组2送风机2运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2送风机2运行",Length=10)]
           public string jz2sfj2yx {get;set;}

           /// <summary>
           /// Desc:机组2送风机1运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="机组2送风机1运行",Length=10)]
           public string jz2sfj1yx {get;set;}

           /// <summary>
           /// Desc:司机室压缩机运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="司机室压缩机运行",Length=10)]
           public string sjsysjyx {get;set;}

           /// <summary>
           /// Desc:司机室冷凝风机运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="司机室冷凝风机运行",Length=10)]
           public string sjslnfjyx {get;set;}

           /// <summary>
           /// Desc:司机室送风机低速运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="司机室送风机低速运行",Length=10)]
           public string sjssfjdsyx {get;set;}

           /// <summary>
           /// Desc:司机室送风机高速运行
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="司机室送风机高速运行",Length=10)]
           public string sjssfjgsyx {get;set;}

            /// <summary>
            /// Desc:是否在线 0 正线  1 离线,
            /// Default:-1
            /// Nullable:False
            /// </summary>           
            [SugarColumn(ColumnDescription="是否在线")]
               public int main_line {get;set;}

    }
}
