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
    public class UserStoryService : IUserStoryService
    {
        private readonly IUserStoryRepo _userStoryRepo;
        private readonly ITasksRepo _taskRepo;
        public UserStoryService(IUserStoryRepo userStoryRepo, ITasksRepo taskRepo)
        {
            _userStoryRepo = userStoryRepo;
            _taskRepo = taskRepo;
        }
        public void AddUserStory(UserStory userStory)
        {
            _userStoryRepo.AddUserStory(userStory);
        }

        public void DeleteUserStory(UserStory userStory)
        {
            _userStoryRepo.DeleteUserStory(userStory);
            _taskRepo.DeleteAllAssociation(userStory.StoryId);
        }

        public List<UserStory> GetAllUserStory()
        {
            return _userStoryRepo.GetAllUserStory();
        }

        public UserStory GetUserStory(int id)
        {
            var userStory = _userStoryRepo.GetUserStory(id);
            return userStory;
        }

        public void UpdateUserStory(UserStory userStory)
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
    }
}
