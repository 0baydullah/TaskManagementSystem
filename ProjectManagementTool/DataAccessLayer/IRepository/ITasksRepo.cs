using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.IRepository
{
    public interface ITasksRepo
    {
        public void AddTasks(Tasks tasks);
        public void UpdateTasks(Tasks tasks);
        public void DeleteTasks(Tasks tasks);
        public Tasks GetTasks(int id);
        public List<Tasks> GetAllTasks();
        public List<Tasks> GetAllTasks(int id);
    }
}
