using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
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

        public SprintService(ISprintRepo sprintRepo)
        {
            _sprintRepo = sprintRepo;
        }
        public bool AddSprint(Sprint sprint)
        {
            try
            {
                var result = _sprintRepo.AddSprint(sprint);
                
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

        public async Task<bool> UpdateSprint(Sprint sprint)
        {
            try
            {
               var result = await _sprintRepo.UpdateSprint(sprint);
               
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
