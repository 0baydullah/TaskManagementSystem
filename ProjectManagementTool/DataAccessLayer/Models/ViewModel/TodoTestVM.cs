using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel;

public class TodoTestVM
{
    public List<Tasks> Tasks { get; set; }
    public List<SubTask> SubTasks { get; set; }
    public Dictionary<int, string> PriorityList { get; set; }
    public Dictionary<int, string> StatusList { get; set; }
}

