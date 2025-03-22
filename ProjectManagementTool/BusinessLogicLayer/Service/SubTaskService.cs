using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using DataAccessLayer.Repository;

namespace BusinessLogicLayer.Service
{
    public class SubTaskService : ISubTaskService
    {
        private readonly ISubTasksRepo _subTaskRepo;

        public SubTaskService(ISubTasksRepo subTaskRepo)
        {
            _subTaskRepo = subTaskRepo;
        }

        public void AddSubTask(SubTask subTask)
        {
            _subTaskRepo.AddSubTask(subTask);
        }

        public void DeleteSubTask(SubTask subTask)
        {
            _subTaskRepo.DeleteSubTask(subTask);
        }

        public List<SubTask> GetAllSubTask()
        {
            var subTask = _subTaskRepo.GetAllSubTask();

            return subTask;
        }

        public List<SubTaskVM> GetAllSubTaskByTask(int id)
        {
            var subTask = _subTaskRepo.GetAllSubTaskByTask(id);

            return subTask;
        }

        public SubTask GetSubTask(int id)
        {
            var subTask = _subTaskRepo.GetSubTask(id);

            return subTask;
        }

        public void UpdateSubTask(SubTask subTasks)
        {
            var existingSubTask = _subTaskRepo.GetSubTask(subTasks.SubTaskId);
            if (existingSubTask != null)
            {
                existingSubTask.Name = subTasks.Name;
                existingSubTask.Descripton = subTasks.Descripton;
                existingSubTask.AssignMembersId = subTasks.AssignMembersId;
                existingSubTask.ReviewerMemberId = subTasks.ReviewerMemberId;
                existingSubTask.EstimatedTime = subTasks.EstimatedTime;
                existingSubTask.Tag = subTasks.Tag;
                existingSubTask.Status = subTasks.Status;
                existingSubTask.Priority = subTasks.Priority;

                _subTaskRepo.UpdateSubTask(existingSubTask);
            }
            else
            {
                throw new ArgumentException("Sub Task not found");
            }
        }
    }
}
