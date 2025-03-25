using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    
    public class SprintRepo : ISprintRepo
    {
        private readonly PMSDBContext _context;
        private readonly ILog log = LogManager.GetLogger(typeof(SprintRepo));
        public SprintRepo(PMSDBContext context)
        {
            _context = context; 
        }

        public bool AddSprint(Sprint sprint)
        {
            try
            {
                var existSprint = _context.Sprints.FirstOrDefault(s => s.SprintName == sprint.SprintName && s.ReleaseId == sprint.ReleaseId);
                if (existSprint == null)
                {
                    _context.Sprints.Add(sprint);
                    _context.SaveChanges();
                   
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteSprint(Sprint sprint)
        {
            try
            {
                _context.Sprints.Remove(sprint);
                _context.SaveChanges();
                
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SprintVM> GetAllSprint(int projectId)
        {
            try
            {
                var sprints = _context.ProjectInfo.Where(project => project.ProjectId == projectId).Join(_context.Releases, p => p.ProjectId, r => r.ProjectId, (p, r) => new  
                {
                    p.ProjectId,
                    r.ReleaseId,
                    r.ReleaseName
                }).Join(_context.Sprints, r => r.ReleaseId, s => s.ReleaseId, (r, s) => new SprintVM
                {
                    SprintId = s.SprintId,
                    SprintName = s.SprintName,
                    Description = s.Description,
                    StartDate = s.StartDate,
                    EndDate = s.StartDate,
                    Points = s.Points,
                    Velocity = s.Velocity,
                    ReleaseName = r.ReleaseName,
                    ReleaseId = r.ReleaseId,
                    Duration = s.Duration
                }).ToList();

                return sprints;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Sprint GetSprint(int id)
        {
            try
            {
                var sprint = _context.Sprints.Find(id);
                if (sprint == null)
                {
                    throw new Exception("Sprint not found!");
                }
                
                return sprint;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Sprint GetSprintByName(int id, int releaseId, string name)
        {
            try
            {
                var sprint = _context.Sprints.FirstOrDefault( s => s.SprintId != id && s.SprintName == name && s.ReleaseId == releaseId );
                return sprint;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateSprint(Sprint sprint)
        {
            try
            {
                _context.Sprints.Update(sprint);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
