using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace DataBase.Tables
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DEVIMGDB")]
    public partial class DEVIMGDB
    {
           public DEVIMGDB(){

            this.createtime =DateTime.Now;
            this.isdel =Convert.ToInt32("0");
            this.sfshow =Convert.ToInt32("0");

           }
           /// <summary>
           /// Desc:主键ID  设备部件表
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnDescription="主键ID  设备部件表")]
           public int id {get;set;}

           /// <summary>
           /// Desc:部件名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="部件名称",Length=255)]
           public string gzbjmc {get;set;}

           /// <summary>
           /// Desc:上浮动
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="上浮动",Length=255)]
           public string top {get;set;}

           /// <summary>
           /// Desc:左浮动
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="左浮动",Length=255)]
           public string left {get;set;}

           /// <summary>
           /// Desc:宽度
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="宽度",Length=255)]
           public string width {get;set;}

           /// <summary>
           /// Desc:高度
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="高度",Length=255)]
           public string height {get;set;}

           /// <summary>
           /// Desc:颜色
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="颜色",Length=255)]
           public string color {get;set;}

           /// <summary>
           /// Desc:透明图
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="透明图",Length=255)]
           public string opacity {get;set;}

           /// <summary>
           /// Desc:状态  0 启用  1禁用
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="状态  0 启用  1禁用",Length=255)]
           public string state {get;set;}

           /// <summary>
           /// Desc:排序
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="排序")]
           public int? sort {get;set;}

           /// <summary>
           /// Desc:背景图片
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="背景图片",Length=255)]
           public string img {get;set;}

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
           /// Desc:上级ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="上级ID")]
           public int? pid {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="描述",Length=255)]
           public string bz {get;set;}

           /// <summary>
           /// Desc:是否删除  0 未删  1删除
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="是否删除  0 未删  1删除")]
           public int? isdel {get;set;}

           /// <summary>
           /// Desc:类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="类型")]
           public int? type {get;set;}

           /// <summary>
           /// Desc:额定寿命（分钟）
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="额定寿命（分钟）")]
           public int? ratedlife {get;set;}

           /// <summary>
           /// Desc:额定次数
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="额定次数")]
           public int? ratenumber {get;set;}

           /// <summary>
           /// Desc:故障类型  0 寿命  1 次数
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障类型  0 寿命  1 次数")]
           public int? faulttype {get;set;}

           /// <summary>
           /// Desc:是否显示  0不显示  1显示
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="是否显示  0不显示  1显示")]
           public int? sfshow {get;set;}

           /// <summary>
           /// Desc:显示的参数
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="显示的参数",Length=255)]
           public string showcs {get;set;}

           /// <summary>
           /// Desc:故障编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnDescription="故障编码",Length=255)]
           public string faultcode {get;set;}

    }
}
