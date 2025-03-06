using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.Models;
using DataAccessLayer.IRepository;

namespace BusinessLogicLayer.Service
{
    public class UserStoryService : IUserStoryService
    {
        private readonly IUserStoryRepo _userStoryRepo;
        public UserStoryService(IUserStoryRepo userStoryRepo)
        {
            _userStoryRepo = userStoryRepo;
        }
        public void AddUserStory(UserStory userStory)
        {
            _userStoryRepo.AddUserStory(userStory);
        }

        public List<UserStory> GetAllUserStory()
        {
            return _userStoryRepo.GetAllUserStory();
        }
    }
}
