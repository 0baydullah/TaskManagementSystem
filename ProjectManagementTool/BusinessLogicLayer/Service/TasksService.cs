using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;

namespace BusinessLogicLayer.Service
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepo _tasksRepo;

        public TasksService(ITasksRepo tasksRepo)
        {
            _tasksRepo = tasksRepo;
        }

        public void AddTasks(Tasks tasks)
        {
            _tasksRepo.AddTasks(tasks);
        }

        public void DeleteTasks(Tasks tasks)
        {
            _tasksRepo.DeleteTasks(tasks);
        }

        public List<Tasks> GetAllTasks()
        {
            var tasks =  _tasksRepo.GetAllTasks();

            return tasks;
        }

        public Tasks GetTasks(int id)
        {
            var task = _tasksRepo.GetTasks(id);

            return task;
        }

        public void UpdateTasks(Tasks tasks)
        {
            var existingTask = _tasksRepo.GetTasks(tasks.TaskId);
            if (existingTask != null)
            {
                existingTask.Name = tasks.Name;
                existingTask.Descripton = tasks.Descripton;
                existingTask.Assignee = tasks.Assignee;
                existingTask.Reviewer = tasks.Reviewer;
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
    }
}
