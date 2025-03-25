using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using log4net;

namespace BusinessLogicLayer.Service
{
    public class BugService : IBugService
    {
        private readonly IBugService _bugService;
        private readonly ILog _log = LogManager.GetLogger(typeof(BugService));
        public BugService(IBugService bugService)
        {
            _bugService = bugService;
        }
        public void AddBug(Bug bug)
        {
            try
            {
                _bugService.AddBug(bug);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw ;
            }
        }

        public void DeleteBug(Bug bug)
        {
            try
            {
                _bugService.DeleteBug(bug);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public List<Bug> GetAllBugOfStory(int id)
        {
            try
            {
                var bugs = _bugService.GetAllBugOfStory(id);
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
                var bug = _bugService.GetBug(id);
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
            throw new NotImplementedException();
        }
    }
}
