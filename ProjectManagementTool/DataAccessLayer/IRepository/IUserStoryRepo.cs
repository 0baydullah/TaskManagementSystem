using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.IRepository
{
    public interface IUserStoryRepo
    {
        public void AddUserStory(UserStory userStory);
        public void UpdateUserStory(UserStory userStory);
        public void DeleteUserStory(UserStory userStory);
        public UserStory GetUserStory(int id);
        public List<UserStory> GetAllUserStory();
    }
}
