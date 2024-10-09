using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("CXH")]
    public partial class CXH
    {
           public CXH(){


           }
           /// <summary>
           /// Desc:车厢id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnDescription="车厢id")]
           public int id {get;set;}

           /// <summary>
           /// Desc:列车id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="列车id")]
           public int? lcid {get;set;}

           /// <summary>
           /// Desc:车厢号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车厢号",Length=50)]
           public string cxh {get;set;}

           /// <summary>
           /// Desc:顺序
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="顺序")]
           public int? sx {get;set;}

           /// <summary>
           /// Desc:创建人id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建人id",Length=50)]
           public string create_userid {get;set;}

           /// <summary>
           /// Desc:创建人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建人名称",Length=50)]
           public string create_username {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建时间")]
           public DateTime? create_time {get;set;}

           /// <summary>
           /// Desc:修改人id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="修改人id",Length=50)]
           public string update_userid {get;set;}

           /// <summary>
           /// Desc:修改人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="修改人名称",Length=50)]
           public string update_username {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="修改时间")]
           public DateTime? update_time {get;set;}

           /// <summary>
           /// Desc:车厢类型  左1：12   右2：21
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车厢类型  左1：12   右2：21")]
           public int? cxtype {get;set;}

           /// <summary>
           /// Desc:车厢类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车厢类型",Length=255)]
           public string cxlx {get;set;}

    }
}
