using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    /// 列车信息表
    ///</summary>
    [SugarTable("LCH")]
    public partial class LCH
    {
           public LCH(){


           }
           /// <summary>
           /// Desc:列车信息表id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnDescription="列车信息表id")]
           public int id {get;set;}

           /// <summary>
           /// Desc:线网id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="线网id",Length=50)]
           public string cocc_id {get;set;}

           /// <summary>
           /// Desc:线网名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="线网名称",Length=50)]
           public string cocc_name {get;set;}

           /// <summary>
           /// Desc:中心id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="中心id",Length=50)]
           public string occ_id {get;set;}

           /// <summary>
           /// Desc:中心名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="中心名称",Length=50)]
           public string occ_name {get;set;}

           /// <summary>
           /// Desc:车辆段id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车辆段id",Length=50)]
           public string dcc_id {get;set;}

           /// <summary>
           /// Desc:车辆段名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车辆段名称",Length=50)]
           public string dcc_name {get;set;}

           /// <summary>
           /// Desc:线路id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="线路id",Length=50)]
           public string xl_id {get;set;}

           /// <summary>
           /// Desc:线路名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="线路名称",Length=50)]
           public string xl_name {get;set;}

           /// <summary>
           /// Desc:车类型id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车类型id",Length=50)]
           public string clx_id {get;set;}

           /// <summary>
           /// Desc:车类型名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车类型名称",Length=50)]
           public string clx_name {get;set;}

           /// <summary>
           /// Desc:车品类型id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车品类型id",Length=50)]
           public string cplx_id {get;set;}

           /// <summary>
           /// Desc:车品类型名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车品类型名称",Length=50)]
           public string cplx_name {get;set;}

           /// <summary>
           /// Desc:列车号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="列车号",Length=50)]
           public string lch {get;set;}

           /// <summary>
           /// Desc:创建用户id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建用户id",Length=50)]
           public string create_userid {get;set;}

           /// <summary>
           /// Desc:创建用户名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建用户名称",Length=50)]
           public string create_username {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
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
           /// Desc:更新用户名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="更新用户名称",Length=50)]
           public string update_username {get;set;}

           /// <summary>
           /// Desc:更新时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="更新时间")]
           public DateTime? update_time {get;set;}

           /// <summary>
           /// Desc:荷载类型：网轨检设备  0带  1不带
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="荷载类型：网轨检设备  0带  1不带")]
           public int? hztype {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="备注",Length=50)]
           public string bz {get;set;}

    }
}
