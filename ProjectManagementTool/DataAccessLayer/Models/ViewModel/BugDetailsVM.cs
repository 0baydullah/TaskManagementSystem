
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class BugDetailsVM
    {
        public Bug Bug { get; set; }
        public string StoryName { get; set; }
        public Dictionary<int, string> StatusList { get; set; }
        public Dictionary<int, string> PriorityList { get; set; }
        public Dictionary<int, string> MemberList { get; set; }
    }
}
