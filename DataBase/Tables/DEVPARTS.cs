using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DEVPARTS")]
    public partial class DEVPARTS
    {
           public DEVPARTS(){

            this.servicelife =Convert.ToInt32("0");
            this.state =Convert.ToInt32("0");
            this.createtime =DateTime.Now;
            this.servicenumber =Convert.ToInt32("0");

           }
           /// <summary>
           /// Desc:主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnDescription="主键ID")]
           public int id {get;set;}

           /// <summary>
           /// Desc:设备ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="设备ID")]
           public int? sbid {get;set;}

           /// <summary>
           /// Desc:零部件ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="零部件ID")]
           public int? lbjid {get;set;}

           /// <summary>
           /// Desc:使用寿命（分钟）
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="使用寿命（分钟）")]
           public int? servicelife {get;set;}

           /// <summary>
           /// Desc:状态  0：使用  1：作废
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="状态  0：使用  1：作废")]
           public int? state {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建时间")]
           public DateTime? createtime {get;set;}

           /// <summary>
           /// Desc:作废时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="作废时间")]
           public DateTime? overduetime {get;set;}

           /// <summary>
           /// Desc:使用次数
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="使用次数")]
           public int? servicenumber {get;set;}

           /// <summary>
           /// Desc:上线时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="上线时间")]
           public DateTime? onlinetime {get;set;}

    }
}
