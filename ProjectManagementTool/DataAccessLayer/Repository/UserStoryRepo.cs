using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using log4net;

namespace DataAccessLayer.Repository
{
    public class UserStoryRepo : IUserStoryRepo
    {
        private readonly PMSDBContext _context;

        private readonly ILog _log = LogManager.GetLogger(typeof(UserStoryRepo));
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
                _log.Error(ex.Message);

                throw new Exception(ex.Message);
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
                _log.Error(ex.Message);

                throw new Exception(ex.Message);
            }
        }

        public List<UserStory> GetAllUserStory()
        {
            try
            {
            //    throw new NullReferenceException("This exception manually  thrown to test exception handled or not");
                return _context.UserStories.ToList();
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
                var userStory = _context.UserStories.First(x => x.StoryId == id);
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
                _context.UserStories.Update(userStory);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                throw new Exception(ex.Message);
            }
        }
    }
}
