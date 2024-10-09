using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    /// 故障项点表
    ///</summary>
    [SugarTable("OVERHAULIDEA")]
    public partial class OVERHAULIDEA
    {
           public OVERHAULIDEA(){

            this.gzztms =Convert.ToString("");
            this.reason =Convert.ToString("");
            this.handle =Convert.ToString("");
            this.create_time =DateTime.Now;
            this.state =Convert.ToString("0");

           }
           /// <summary>
           /// Desc:主键ID  项点表
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnDescription="主键ID  项点表")]
           public int id {get;set;}

           /// <summary>
           /// Desc:项点名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="项点名称",Length=255)]
           public string jxname {get;set;}

           /// <summary>
           /// Desc:阀值单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="阀值单位",Length=255)]
           public string fzdw {get;set;}

           /// <summary>
           /// Desc:计算方式  1.不在范围值内（小于或大于）2.取绝对值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="计算方式  1.不在范围值内（小于或大于）2.取绝对值",Length=255)]
           public string jsfs {get;set;}

           /// <summary>
           /// Desc:故障状态描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障状态描述",Length=255)]
           public string gzztms {get;set;}

           /// <summary>
           /// Desc:故障原因
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障原因",Length=255)]
           public string reason {get;set;}

           /// <summary>
           /// Desc:司机处理建议
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="司机处理建议",Length=255)]
           public string handle {get;set;}

           /// <summary>
           /// Desc:空调类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="空调类型",Length=255)]
           public string kttype {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="备注",Length=255)]
           public string bz {get;set;}

           /// <summary>
           /// Desc:范围1  起始
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="范围1  起始",Length=255)]
           public string range1 {get;set;}

           /// <summary>
           /// Desc:范围2  结束
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="范围2  结束",Length=255)]
           public string range2 {get;set;}

           /// <summary>
           /// Desc:建立用户id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="建立用户id",Length=50)]
           public string create_userid {get;set;}

           /// <summary>
           /// Desc:修改用户ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="修改用户ID",Length=50)]
           public string update_userid {get;set;}

           /// <summary>
           /// Desc:建立时间
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="建立时间")]
           public DateTime? create_time {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="修改时间")]
           public DateTime? update_time {get;set;}

           /// <summary>
           /// Desc:状态 
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="状态 ",Length=255)]
           public string state {get;set;}

           /// <summary>
           /// Desc:类型  1:提示  2:预警  3:报警
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="类型  1:提示  2:预警  3:报警",Length=255)]
           public string type {get;set;}

           /// <summary>
           /// Desc:所属故障类型ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="所属故障类型ID",Length=255)]
           public string gzlxid {get;set;}

           /// <summary>
           /// Desc:故障等级
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障等级",Length=255)]
           public string gzdj {get;set;}

           /// <summary>
           /// Desc:故障部位
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障部位",Length=255)]
           public string gzbw {get;set;}

           /// <summary>
           /// Desc:故障值（对应v1-v72）
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障值（对应v1-v72）",Length=255)]
           public string gzval {get;set;}

           /// <summary>
           /// Desc:故障编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障编码",Length=255)]
           public string faultcode {get;set;}

           /// <summary>
           /// Desc:回库检修建议
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="回库检修建议",Length=255)]
           public string overhaul {get;set;}

    }
}
