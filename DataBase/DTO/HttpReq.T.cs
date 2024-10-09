using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DTO
{
    public class HttpReq<T> : HttpReq
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T? result_data { get; set; }
    }
}
