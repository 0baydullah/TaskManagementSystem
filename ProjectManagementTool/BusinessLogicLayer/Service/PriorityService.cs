using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using log4net;

namespace BusinessLogicLayer.Service
{
    public class PriorityService : IPriorityService
    {
        private readonly IPriorityRepo _priorityRepo;
        private readonly ILog _log = LogManager.GetLogger(typeof(StatusService));

        public PriorityService(IPriorityRepo priorityRepo)
        {
            _priorityRepo = priorityRepo;
        }

        public void AddPriority(Priority priority)
        {
            try
            {
                _priorityRepo.AddPriority(priority);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void DeletePriority(int id)
        {
            try
            {
                _priorityRepo.DeletePriority(id);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public List<Priority> GetAllPriority()
        {
            try
            {
                var priority = _priorityRepo.GetAllPriorities();

                return priority;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public Priority GetPriorityById(int id)
        {
            try
            {
                var priority = _priorityRepo.GetPriorityById(id);

                return priority;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void UpdatePriority(Priority priority)
        {
            try
            {
                _priorityRepo.UpdatePriority(priority);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }
    }
}
