using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class TaskDetailsVM
    {
        public Tasks Tasks { get; set; }
        public string StoryName {get;set;}
        public List<SubTaskVM> SubTask { get; set; }        public Dictionary<int,string> StatusList { get; set; }
        public Dictionary<int,string> PriorityList { get; set; }
        public Dictionary<int,string> MemberList { get; set; }
    }
}
