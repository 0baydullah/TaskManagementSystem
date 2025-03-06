using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;

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
            _context.UserStories.Add(userStory);
            _context.SaveChanges();
        }

        public List<UserStory> GetAllUserStory()
        {
            return _context.UserStories.ToList();
        }
    }
}
