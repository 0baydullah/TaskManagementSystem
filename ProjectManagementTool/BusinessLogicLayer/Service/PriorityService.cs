using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;

namespace BusinessLogicLayer.Service
{
    public class PriorityService : IPriorityService
    {
        private readonly IPriorityRepo _priorityRepo;

        public PriorityService(IPriorityRepo priorityRepo)
        {
            _priorityRepo = priorityRepo;
        }

        public void AddPriority(Priority priority)
        {
            _priorityRepo.AddPriority(priority);
        }

        public void DeletePriority(int id)
        {
            _priorityRepo.DeletePriority(id);
        }

        public List<Priority> GetAllPriority()
        {
            var priority = _priorityRepo.GetAllPriorities();

            return priority;
        }

        public Priority GetPriorityById(int id)
        {
            var priority = _priorityRepo.GetPriorityById(id);

            return priority;
        }

        public void UpdatePriority(Priority priority)
        {
            _priorityRepo.UpdatePriority(priority);
        }
    }
}
