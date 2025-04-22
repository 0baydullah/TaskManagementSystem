using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class TodoBugVM
    {
        public List<Bug> Bug { get; set; } = new List<Bug>();
        public Dictionary<int, string> PriorityList { get; set; }
        public Dictionary<int, string> BugStatusList { get; set; }
    }
}
