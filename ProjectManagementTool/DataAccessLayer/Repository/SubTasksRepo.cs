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

        public void AddSubTasks(Tasks tasks)
        {
            try
            {
                _context.SubTask.Add(tasks);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while adding the task.", ex);
            }
        }

        public void DeleteSubTasks(Tasks tasks)
        {
            try
            {
                _context.Tasks.Remove(tasks);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the task.", ex);
            }
        }

        public List<Tasks> GetAllSubTasks()
        {
            try
            {
                var tasks = _context.Tasks.ToList();

                return tasks;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all tasks.", ex);
            }
        }

        public List<Tasks> GetAllSubTasksByTask(int id)
        {
            try
            {
                var tasks = _context.Tasks.Where(x=>x.UserStoryId == id ).ToList();

                return tasks;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all tasks.", ex);
            }
        }

        public Tasks GetSubTasks(int id)
        {
            try
            {
                var task = _context.Tasks.FirstOrDefault(x => x.TaskId == id);
                return task;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the task with ID {id}.", ex);
            }
        }

        public void UpdateSubTasks(Tasks tasks)
        {
            try
            {
                _context.Tasks.Update(tasks);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the task.", ex);
            }
        }
    }
}
