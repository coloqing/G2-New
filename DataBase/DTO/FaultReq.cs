using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DTO
{
    /// <summary>
    /// 故障推送返回数据
    /// </summary>
    public class FaultReq
    {
        public long[] new_faults {  get; set; }
        public long[] end_faults {  get; set; }
    }
}
