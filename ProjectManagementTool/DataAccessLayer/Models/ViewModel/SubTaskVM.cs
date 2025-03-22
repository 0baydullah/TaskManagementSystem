using DataAccessLayer.Enums;
using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class SubTaskVM
    {
        [Key]
        public int SubTaskId { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public int AssignMembersId { get; set; }
        public int ReviewerMemberId { get; set; }
        public double EstimatedTime { get; set; }
        public string Tag { get; set; }
        public Statusx Status { get; set; }
        public Priorityx Priority { get; set; }
        public int TaskId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double TotalTime { get; set; }   
    }
}
