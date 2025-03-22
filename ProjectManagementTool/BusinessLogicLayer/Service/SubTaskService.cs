using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Repository;
using log4net;

namespace BusinessLogicLayer.Service
{
    public class SubTaskService : ISubTaskService
    {
        private readonly ISubTasksRepo _subTaskRepo;

        private readonly ILog _log = LogManager.GetLogger(typeof(SubTaskService));

        public SubTaskService(ISubTasksRepo subTaskRepo)
        {
            _subTaskRepo = subTaskRepo;
        }

        public void AddSubTask(SubTask subTask)
        {
            try
            {
                _subTaskRepo.AddSubTask(subTask);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeleteSubTask(SubTask subTask)
        {
            try
            {
                _subTaskRepo.DeleteSubTask(subTask);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public List<SubTask> GetAllSubTask()
        {
            try
            {
                var subTask = _subTaskRepo.GetAllSubTask();
                return subTask;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public List<SubTask> GetAllSubTaskByTask(int id)
        {
            try
            {
                var subTask = _subTaskRepo.GetAllSubTaskByTask(id);
                return subTask;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public SubTask GetSubTask(int id)
        {
            try
            {
                var subTask = _subTaskRepo.GetSubTask(id);
                return subTask;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }

        public void UpdateSubTask(SubTask subTasks)
        {
            try
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
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
