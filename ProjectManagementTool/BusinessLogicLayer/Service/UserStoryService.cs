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
    public class UserStoryService : IUserStoryService
    {
        private readonly IUserStoryRepo _userStoryRepo;
        private readonly ITasksRepo _taskRepo;
        private readonly ILog _log = LogManager.GetLogger(typeof(UserStoryService));
        public UserStoryService(IUserStoryRepo userStoryRepo, ITasksRepo taskRepo)
        {
            _userStoryRepo = userStoryRepo;
            _taskRepo = taskRepo;
        }
        public void AddUserStory(UserStory userStory)
        {
            try
            {
                _userStoryRepo.AddUserStory(userStory);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                throw new Exception(ex.Message);
            }
        }

        public void DeleteUserStory(UserStory userStory)
        {
            try
            {
                _userStoryRepo.DeleteUserStory(userStory);
                _taskRepo.DeleteAllAssociation(userStory.StoryId);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                throw new Exception(ex.Message);
            }
        }

        public List<UserStory> GetAllUserStory()
        {
            try
            {
                return _userStoryRepo.GetAllUserStory();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                throw new Exception(ex.Message);
            }
        }

        public UserStory GetUserStory(int id)
        {
            try
            {
                var userStory = _userStoryRepo.GetUserStory(id);
                return userStory;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                throw new Exception(ex.Message);
            }
        }

        public void UpdateUserStory(UserStory userStory)
        {
            try
            {
                var existingUserStory = _userStoryRepo.GetUserStory(userStory.StoryId);
                if (existingUserStory != null)
                {
                    existingUserStory.StoryName = userStory.StoryName;
                    existingUserStory.Description = userStory.Description;
                    existingUserStory.Category = userStory.Category;
                    existingUserStory.Points = userStory.Points;
                    existingUserStory.EstimateTime = userStory.EstimateTime;
                    existingUserStory.Status = userStory.Status;
                    existingUserStory.Priority = userStory.Priority;
                    existingUserStory.SprintId = userStory.SprintId;

                    _userStoryRepo.UpdateUserStory(existingUserStory);
                }
                else
                {
                    throw new ArgumentException("User story not found");
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                throw new Exception(ex.Message);
            }
        }
    }
}
