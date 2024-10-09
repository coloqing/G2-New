using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    /// 配置表
    ///</summary>
    [SugarTable("SYS_CONFIG")]
    public partial class SYS_CONFIG
    {
           public SYS_CONFIG(){

            this.state =Convert.ToString("1");
            this.createtime =DateTime.Now;

           }
           /// <summary>
           /// Desc:主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnDescription="主键ID")]
           public int conid {get;set;}

           /// <summary>
           /// Desc:项编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="项编码",Length=255)]
           public string concode {get;set;}

           /// <summary>
           /// Desc:项名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="项名称",Length=255)]
           public string conname {get;set;}

           /// <summary>
           /// Desc:值
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="值",Length=255)]
           public string conval {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:1
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="状态",Length=255)]
           public string state {get;set;}

           /// <summary>
           /// Desc:排序
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="排序")]
           public int? seqvalue {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建时间")]
           public DateTime? createtime {get;set;}

           /// <summary>
           /// Desc:用户ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="用户ID",Length=255)]
           public string userid {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="描述",Length=500)]
           public string describe {get;set;}

    }
}
