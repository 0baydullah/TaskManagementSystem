using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class TodoTaskVM
    {
        public List<Tasks> Tasks { get; set; } = new List<Tasks>();
        public List<SubTask>? SubTasks { get; set; } = new List<SubTask>();

        public Dictionary<int, string> PriorityList { get; set; }
    }
}
