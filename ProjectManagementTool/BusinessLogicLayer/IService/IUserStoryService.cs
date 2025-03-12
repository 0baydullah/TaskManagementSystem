using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;

namespace BusinessLogicLayer.IService
{
    public interface IUserStoryService
    {
        public void AddUserStory(UserStory userStory);
        public void UpdateUserStory(UserStory userStory);
        public void DeleteUserStory(UserStory userStory);
        public UserStory GetUserStory(int id);
        public List<UserStory> GetAllUserStory();
    }
}
