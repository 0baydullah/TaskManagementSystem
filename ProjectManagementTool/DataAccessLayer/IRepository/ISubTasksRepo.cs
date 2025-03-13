using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.IRepository
{
    public interface ISubTasksRepo
    {
        public void AddSubTasks(SubTask subTask);
        public void UpdateSubTasks(SubTask subTasks);
        public void DeleteSubTasks(SubTask subTask);
        public SubTask GetSubTasks(int id);
        public List<SubTask> GetAllSubTasks();
        public List<SubTask> GetAllSubTasksByTask(int id);
    }
}
