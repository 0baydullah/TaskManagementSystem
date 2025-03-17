using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.IRepository
{
    public interface ISubTasksRepo
    {
        public void AddSubTask(SubTask subTask);
        public void UpdateSubTask(SubTask subTask);
        public void DeleteSubTask(SubTask subTask);
        public void DeleteAllAssociation(int id);
        public SubTask GetSubTask(int id);
        public List<SubTask> GetAllSubTask();
        public List<SubTask> GetAllSubTaskByTask(int id);
    }
}
