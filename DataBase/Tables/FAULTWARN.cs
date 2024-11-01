using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    /// 空调状态实时信息（故障表）
    ///</summary>
    [SugarTable("FAULTWARN")]
    public partial class FAULTWARN
    {
           public FAULTWARN(){

            this.state =Convert.ToString("0");
            this.createtime =DateTime.Now;
            this.sfxztb =Convert.ToString("0");
            this.sfgbtb =Convert.ToString("0");
            this.sjsczt =Convert.ToString("0");
            this.ptfaultid =Convert.ToInt64("0");
            this.ptfaultidtb =Convert.ToByte("0");

           }
           /// <summary>
           /// Desc:主键ID  故障表
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIdentity=true, IsPrimaryKey = true,ColumnDescription = "主键ID  故障表")]
           public int id {get;set;}

           /// <summary>
           /// Desc:城市ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="城市ID")]
           public int? city_id {get;set;}

           /// <summary>
           /// Desc:城市名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="城市名称",Length=255)]
           public string city_mc {get;set;}

           /// <summary>
           /// Desc:线网id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="线网id")]
           public int? cocc_id {get;set;}

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
           [SugarColumn(ColumnDescription="中心id")]
           public int? occ_id {get;set;}

           /// <summary>
           /// Desc:中心名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="中心名称",Length=50)]
           public string occ_name {get;set;}

           /// <summary>
           /// Desc:线路ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="线路ID")]
           public int? line_id {get;set;}

           /// <summary>
           /// Desc:线路名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="线路名称",Length=255)]
           public string line_mc {get;set;}

           /// <summary>
           /// Desc:列车号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="列车号",Length=255)]
           public string lch {get;set;}

           /// <summary>
           /// Desc:列车ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="列车ID")]
           public int? lcid {get;set;}

           /// <summary>
           /// Desc:车厢号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车厢号",Length=255)]
           public string cxh {get;set;}

           /// <summary>
           /// Desc:车厢ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="车厢ID")]
           public int? cxid {get;set;}

           /// <summary>
           /// Desc:设备编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="设备编码",Length=255)]
           public string sbbm {get;set;}

           /// <summary>
           /// Desc:设备ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="设备ID")]
           public int? sbid {get;set;}

           /// <summary>
           /// Desc:项点ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="项点ID")]
           public int? xdid {get;set;}

           /// <summary>
           /// Desc:项点名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="项点名称",Length=255)]
           public string xdmc {get;set;}

           /// <summary>
           /// Desc:类型  1:提示  2:预警  3:报警
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="类型  1:提示  2:预警  3:报警",Length=255)]
           public string type {get;set;}

           /// <summary>
           /// Desc:故障级别  1:轻微  2:中等  3: 严重
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障级别  1:轻微  2:中等  3: 严重",Length=255)]
           public string gzjb {get;set;}

           /// <summary>
           /// Desc:状态  0  未维修  1  已维修   2  关闭
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="状态  0  未维修  1  已维修   2  关闭",Length=255)]
           public string state {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建时间")]
           public DateTime? createtime {get;set;}

           /// <summary>
           /// Desc:创建人
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建人",Length=50)]
           public string createuserid {get;set;}

           /// <summary>
           /// Desc:创建人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="创建人名称",Length=255)]
           public string createusermc {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="修改时间")]
           public DateTime? updatetime {get;set;}

           /// <summary>
           /// Desc:修改人
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="修改人",Length=50)]
           public string updateuserid {get;set;}

           /// <summary>
           /// Desc:修改人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="修改人名称",Length=50)]
           public string updateusermc {get;set;}

           /// <summary>
           /// Desc:故障部件（1 机组1、2 机组2 3其他）
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障部件（1 机组1、2 机组2 3其他）",Length=255)]
           public string gzbj {get;set;}

           /// <summary>
           /// Desc:故障部件名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障部件名称",Length=50)]
           public string gzbjmc {get;set;}

           /// <summary>
           /// Desc:类型名称  1:提示  2:预警  3:报警
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="类型名称  1:提示  2:预警  3:报警",Length=255)]
           public string typemc {get;set;}

           /// <summary>
           /// Desc:故障级别  1:轻微  2:中等  3: 严重
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障级别  1:轻微  2:中等  3: 严重",Length=255)]
           public string gzjbmc {get;set;}

           /// <summary>
           /// Desc:采集时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="采集时间")]
           public DateTime? collect_time {get;set;}

           /// <summary>
           /// Desc:是否新增同步（地面接口）改：同步id
           /// Default:0
           /// Nullable:False
           /// </summary>           
           [SugarColumn(ColumnDescription="是否新增同步（地面接口）",Length=10)]
           public string sfxztb {get;set;}

           /// <summary>
           /// Desc:是否关闭同步（地面接口）
           /// Default:0
           /// Nullable:False
           /// </summary>           
           [SugarColumn(ColumnDescription="是否关闭同步（地面接口）",Length=10)]
           public string sfgbtb {get;set;}

           /// <summary>
           /// Desc:事件上传状态（物模型：0未上传，1已上传）
           /// Default:0
           /// Nullable:False
           /// </summary>           
           [SugarColumn(ColumnDescription="事件上传状态（物模型：0未上传，1已上传）",Length=10)]
           public string sjsczt {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           [SugarColumn(ColumnDescription="")]
           public long ptfaultid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           [SugarColumn(ColumnDescription="")]
           public byte ptfaultidtb {get;set;}

    }
}
