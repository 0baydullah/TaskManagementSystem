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

        public void AddSprint(Sprint sprint)
        {
            try
            {
                var existSprint = _context.Sprints.FirstOrDefault(s => s.SprintName == sprint.SprintName && s.ReleaseId == sprint.ReleaseId);
                if (existSprint == null)
                {
                    _context.Sprints.Add(sprint);
                    _context.SaveChanges();
                }
                else
                {
                    log.Error("The sprint already exists.");
                    
                    //throw new Exception("The sprint already exists.");
                }

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while adding the sprint!", ex);
            }
        }

        public void DeleteSprint(Sprint sprint)
        {
            try
            {
                _context.Sprints.Remove(sprint);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while deleting the sprint!", ex);
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
            catch (Exception ex)
            {

                throw new Exception("An error occurred while list all the sprint!", ex);
            }
        }

        public Sprint GetSprint(int id)
        {
            try
            {
                var sprint = _context.Sprints.Find(id);
                return sprint;

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while getting the sprint!", ex);
            }
        }
        public void UpdateSprint(Sprint sprint)
        {
            try
            {
                _context.Sprints.Update(sprint);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while getting the sprint!", ex);
            }
        }
    }
}
