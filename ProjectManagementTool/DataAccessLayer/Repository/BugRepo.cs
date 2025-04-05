using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class BugRepo : IBugRepo
    {
        private readonly PMSDBContext _context;
        private readonly ILog _log = LogManager.GetLogger(typeof(BugRepo));
        public BugRepo(PMSDBContext context)
        {
            _context = context;
        }
        public void AddBug(Bug bug)
        {
            try
            {
                _context.Bugs.Add(bug);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void DeleteBug(Bug bug)
        {
            try
            {
                _context.Bugs.Remove(bug);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public List<Bug> GetAllBugOfStory(int id)
        {
            try
            {
                var bugs = _context.Bugs.Where(x => x.UserStoryId == id).ToList();

                return bugs;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public Bug GetBug(int id)
        {
            try
            {
                var bug = _context.Bugs.AsNoTracking().FirstOrDefault(b => b.BugId == id);
                return bug;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void UpdateBug(Bug bug)
        {
            try
            {
                _context.Bugs.Update(bug);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }
    }
}
