using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;

namespace BusinessLogicLayer.IService
{
    public interface ITasksService
    {
        public void AddTasks(Tasks tasks);
        public void UpdateTasks(Tasks tasks);
        public void DeleteTasks(Tasks tasks);
        public Tasks GetTasks(int id);
        public List<Tasks> GetAllTasks();
        public List<Tasks> GetAllTasksByMember(List<Member>? members);
        public List<TasksVM> GetAllTasks(int id);
    }
}
