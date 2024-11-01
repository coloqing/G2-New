using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DTO
{
    public class HttpReq
    {
        public bool success { get; set; }
        public string result_code { get; set; }
        public string result_msg { get; set; }

    }

    public class HttpWarnReq
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
