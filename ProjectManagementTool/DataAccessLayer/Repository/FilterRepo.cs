using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class FilterRepo : IFilterRepo
    {
        private readonly PMSDBContext _context;

        public FilterRepo(PMSDBContext context)
        {
            _context = context;
        }

        public void InProgress(string status, int projectId)
        {
            var statuses = _context.Statuses.FirstOrDefault(x => x.Name == status);
            var statusId = statuses.StatusId;
            var stories = _context.UserStories.Where(s => s.Status == statusId && s.ProjectId == projectId).ToList();
            var tasks = _context.Tasks.Where(s => s.Status == statusId).ToList();
            var subtasks = _context.SubTask.Where(s => s.Status == statusId).ToList();
            var bug = _context.Bugs.Where(s => s.BugStatus == statusId).ToList();
            
            throw new NotImplementedException();
        }
    }
}
