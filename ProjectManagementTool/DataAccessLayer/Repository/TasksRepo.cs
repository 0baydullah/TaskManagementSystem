using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using log4net;

namespace DataAccessLayer.Repository
{
    public class TasksRepo : ITasksRepo
    {
        private readonly PMSDBContext _context;
        private readonly ILog _log = LogManager.GetLogger(typeof(TasksRepo));

        public TasksRepo(PMSDBContext context)
        {
            _context = context;
        }

        public void AddTasks(Tasks tasks)
        {
            try
            {
                _context.Tasks.Add(tasks);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteTasks(Tasks tasks)
        {
            try
            {
                _context.Tasks.Remove(tasks);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAllAssociation(int id)
        {
            try
            {
                var tasks = _context.Tasks.Where(x => x.UserStoryId == id).ToList();
                _context.Tasks.RemoveRange(tasks);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<Tasks> GetAllTasks()
        {
            try
            {
                var tasks = _context.Tasks.ToList();
                return tasks;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<Tasks> GetAllTasks(int id)
        {
            try
            {
                var tasks = _context.Tasks.Where(x => x.UserStoryId == id).ToList();
                return tasks;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Tasks GetTasks(int id)
        {
            try
            {
                var task = _context.Tasks.FirstOrDefault(x => x.TaskId == id);
                return task;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateTasks(Tasks tasks)
        {
            try
            {
                _context.Tasks.Update(tasks);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
