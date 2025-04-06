using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepo _sprintRepo;
        private readonly IUserStoryRepo _userStoryRepo;

        public SprintService(ISprintRepo sprintRepo, IUserStoryRepo userStoryRepo)
        {
            _sprintRepo = sprintRepo;
            _userStoryRepo = userStoryRepo;
        }
        public bool AddSprint(SprintCreateVM sprint)
        {
            try
            {
                var data = new Sprint()
                {
                    SprintName = sprint.SprintName,
                    Description = sprint.Description,
                    StartDate = sprint.StartDate,
                    Duration = sprint.Duration,
                    Points = sprint.Points,
                    Velocity = sprint.Velocity,
                    ReleaseId = sprint.ReleaseId,
                };
                var result = _sprintRepo.AddSprint(data);
                
                return result;
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
                var result  = _sprintRepo.DeleteSprint(sprint);

                return result;
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
                var sprints = _sprintRepo.GetAllSprint(projectId);
                
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
                var sprint = _sprintRepo.GetSprint(id);
                if( sprint == null)
                {
                    throw new Exception("Sprint not found! ");
                }
                
                return sprint;
            }
            catch (Exception)
            {
                throw;
            }
          
        }

        public SprintDetailsVM GetSprintDetails(int id)
        {
            try
            {
                var sprint = _sprintRepo.GetSprint(id);
                var story = _userStoryRepo.GetAllUserStory().Where( u => u.SprintId == id).ToList();
                var sprintDetails = new SprintDetailsVM
                {
                    SprintId = sprint.SprintId,
                    SprintName = sprint.SprintName,
                    Description = sprint.Description,
                    StartDate =  sprint.StartDate,
                    Duration = sprint.Duration,
                    //EndDate = 
                    Points = sprint.Points,
                    Velocity = sprint.Velocity,
                    UserStroy = story
                };
                return sprintDetails;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateSprint(int id, SprintCreateVM sprint)
        {
            try
            {
                var existSprint = _sprintRepo.GetSprint(id);
                var existSprintName = _sprintRepo.GetSprintByName(id, sprint.ReleaseId, sprint.SprintName);

                if( existSprint == null || existSprintName != null)
                {
                    return false;
                }

                existSprint.SprintName = sprint.SprintName;
                existSprint.Description = sprint.Description;
                existSprint.StartDate = sprint.StartDate;
                existSprint.Duration = sprint.Duration;
                existSprint.Points = sprint.Points;
                existSprint.Velocity = sprint.Velocity;
                existSprint.ReleaseId = sprint.ReleaseId;

                var result = await _sprintRepo.UpdateSprint(existSprint);
               
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
