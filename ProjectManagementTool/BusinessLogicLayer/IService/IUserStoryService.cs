using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.IService
{
    public interface IUserStoryService
    {
        public void AddUserStory(UserStory userStory);
        //public void UpdateUserStory();
        //public void DeleteUserStory();
        //public UserStory GetUserStory();
        public List<UserStory> GetAllUserStory();
    }
}
