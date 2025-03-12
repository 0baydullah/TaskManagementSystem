using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Repository
{
    public class UserStoryRepo : IUserStoryRepo
    {
        private readonly PMSDBContext _context;
        public UserStoryRepo(PMSDBContext context)
        {
            _context = context;
        }
        public void AddUserStory(UserStory userStory)
        {
            try
            {
                _context.UserStories.Add(userStory);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the user story.", ex);
            }
        }

        public void DeleteUserStory(UserStory userStory)
        {
            try
            {
                _context.UserStories.Remove(userStory);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user story.", ex);
            }
        }

        public List<UserStory> GetAllUserStory()
        {
            try
            {
                return _context.UserStories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all user stories.", ex);
            }
        }

        public UserStory GetUserStory(int id)
        {
            try
            {
                var userStory = _context.UserStories.FirstOrDefault(x => x.StoryId == id);
                return userStory;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the user story with ID {id}.", ex);
            }
        }

        public void UpdateUserStory(UserStory userStory)
        {
            try
            {
                _context.UserStories.Update(userStory);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user story.", ex);
            }
        }
    }
}
