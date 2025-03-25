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
    public class StatusService : IStatusService
    {
        private readonly IStatusRepo _statusRepo;
        private readonly ILog _log = LogManager.GetLogger(typeof(StatusService));

        public StatusService(IStatusRepo statusRepo)
        {
            _statusRepo = statusRepo;
        }

        public void AddStatus(Status status)
        {
            try
            {
                _statusRepo.AddStatus(status);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void DeleteStatus(int id)
        {
            try
            {
                _statusRepo.DeleteStatus(id);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public List<Status> GetAllStatuses()
        {
            try
            {
                var statuses = _statusRepo.GetAllStatuses();
                return statuses;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public Status GetStatusById(int id)
        {
            try
            {
                var status = _statusRepo.GetStatusById(id);
                return status;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void UpdateStatus(Status status)
        {
            try
            {
                _statusRepo.UpdateStatus(status);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }
    }
}
