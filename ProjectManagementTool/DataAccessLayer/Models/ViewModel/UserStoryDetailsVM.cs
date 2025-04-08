using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class UserStoryDetailsVM
    {
        public int MemberId { get; set; } 
        public List<int> AdminMemberIds { get; set; }
        public UserStory Story { get; set; }
        public List<TasksVM> Tasks { get; set; }
        public List<Bug> Bugs { get; set; }
        public Dictionary<int,string> StatusList { get; set; }
        public Dictionary<int,string> PriorityList { get; set; }
        public Dictionary<int,string> CategoryList { set; get; }
        public Dictionary<int,string> MemberList { get; set; }
        public Dictionary<int,string> SprintList { get; set; }
    }
}
