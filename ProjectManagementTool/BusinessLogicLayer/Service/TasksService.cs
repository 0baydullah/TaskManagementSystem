using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using log4net;

namespace BusinessLogicLayer.Service
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepo _tasksRepo;
        private readonly ISubTasksRepo _subTasksRepo;

        private readonly ILog _log = LogManager.GetLogger(typeof(TasksService));


        public TasksService(ITasksRepo tasksRepo, ISubTasksRepo subTasksRepo)
        {
            _tasksRepo = tasksRepo;
            _subTasksRepo = subTasksRepo;
        }

        public void AddTasks(Tasks tasks)
        {
            try
            {
                _tasksRepo.AddTasks(tasks);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeleteTasks(Tasks tasks)
        {
            try
            {
                _tasksRepo.DeleteTasks(tasks);
                _subTasksRepo.DeleteAllAssociation(tasks.TaskId);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public List<Tasks> GetAllTasks()
        {
            try
            {
                var tasks = _tasksRepo.GetAllTasks();
                return tasks;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public List<Tasks> GetAllTasks(int id)
        {
            try
            {
                var tasks = _tasksRepo.GetAllTasks(id);
                return tasks;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public Tasks GetTasks(int id)
        {
            try
            {
                var task = _tasksRepo.GetTasks(id);
                return task;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public void UpdateTasks(Tasks tasks)
        {
            try
            {
                var existingTask = _tasksRepo.GetTasks(tasks.TaskId);
                if (existingTask != null)
                {
                    existingTask.Name = tasks.Name;
                    existingTask.Descripton = tasks.Descripton;
                    existingTask.AssignMembersId = tasks.AssignMembersId;
                    existingTask.ReviewerMemberId = tasks.ReviewerMemberId;
                    existingTask.EstimatedTime = tasks.EstimatedTime;
                    existingTask.Tag = tasks.Tag;
                    existingTask.Status = tasks.Status;
                    existingTask.Priority = tasks.Priority;

                    _tasksRepo.UpdateTasks(existingTask);
                }
                else
                {
                    throw new ArgumentException("Task not found");
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
