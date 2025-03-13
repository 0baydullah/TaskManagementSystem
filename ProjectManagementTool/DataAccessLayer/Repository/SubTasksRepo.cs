using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Repository
{
    public class SubTasksRepo : ISubTasksRepo
    {
        private readonly PMSDBContext _context;

        public SubTasksRepo(PMSDBContext context)
        {
            _context = context;
        }

        public void AddSubTask(SubTask subTask)
        {
            try
            {
                _context.SubTask.Add(subTask);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while adding the sub task.", ex);
            }
        }

        public void DeleteSubTask(SubTask subTask)
        {
            try
            {
                _context.SubTask.Remove(subTask);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the sub task.", ex);
            }
        }

        public List<SubTask> GetAllSubTask()
        {
            try
            {
                var subTask = _context.SubTask.ToList();

                return subTask;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all sub tasks.", ex);
            }
        }

        public List<SubTask> GetAllSubTaskByTask(int id)
        {
            try
            {
                var subTask = _context.SubTask.Where(x=>x.TaskId == id ).ToList();

                return subTask;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all tasks.", ex);
            }
        }

        public SubTask GetSubTask(int id)
        {
            try
            {
                var subTask = _context.SubTask.FirstOrDefault(x => x.SubTaskId == id);
                return subTask;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the task with ID {id}.", ex);
            }
        }

        public void UpdateSubTask(SubTask subTask)
        {
            try
            {
                _context.SubTask.Update(subTask);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the task.", ex);
            }
        }
    }
}
