using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.DTO
{
    /// <summary>
    /// 预警推送
    /// </summary>
    public class WarnPushDTO
    {
        public string? id {  get; set; }
        public string? message_type {  get; set; }
        public string? subsystem {  get; set; }
        public string? train_type {  get; set; }
        public string? train_no {  get; set; }
        public string? coach {  get; set; }
        public string? location {  get; set; }
        public string? code {  get; set; }
        public string? station1 {  get; set; }
        public string? station2 {  get; set; }
        public string? starttime {  get; set; }
        public string? endtime { get; set; }

    }
}
