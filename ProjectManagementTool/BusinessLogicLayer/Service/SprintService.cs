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
        public void AddSprint(Sprint sprint)
        {
            _sprintRepo.AddSprint(sprint);
        }

        public void DeleteSprint(Sprint sprint)
        {
            _sprintRepo.DeleteSprint(sprint);
        }

        public List<SprintVM> GetAllSprint(int projectId)
        {
           var sprints = _sprintRepo.GetAllSprint(projectId);
           return sprints;
        }

        public Sprint GetSprint(int id)
        {
           var sprint = _sprintRepo.GetSprint(id);
           return sprint;
        }

        public void UpdateSprint(Sprint sprint)
        {
            _sprintRepo.UpdateSprint(sprint);
        }
    }
}
