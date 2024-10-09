using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DTO
{
    public class LifeResDTO
    {
        public string line_code { get; set; }
        public string train_code { get; set; }
        public string coach_no { get; set; }
        public string life_code { get; set; }
        public string prediction_time { get; set; }
        public string residual_kilometre { get; set; }
        public string running_kilometre { get; set; }
        public string residual_day { get; set; }
        public string running_day { get; set; }
        public string part_url { get; set; }
        public string part_score { get; set; }
        public string residual_remark { get; set; }
    }

    public class LifeHttpResDTO
    {
        public string app_id { get; set; }
        public string app_token { get; set; }

        public List<LifeResDTO> lifes { get; set; }
    }
}
