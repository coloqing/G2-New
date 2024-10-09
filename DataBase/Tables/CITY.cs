using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("CITY")]
    public partial class CITY
    {
           public CITY(){

            this.state =Convert.ToInt32("0");

           }
           /// <summary>
           /// Desc:城市主键id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnDescription="城市主键id")]
           public int id {get;set;}

           /// <summary>
           /// Desc:城市名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="城市名称",Length=50)]
           public string cityname {get;set;}

           /// <summary>
           /// Desc:顺序
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="顺序")]
           public int? sort {get;set;}

           /// <summary>
           /// Desc:是否启用，（0启用，1禁用）
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="是否启用，（0启用，1禁用）")]
           public int? state {get;set;}

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

    }
}
