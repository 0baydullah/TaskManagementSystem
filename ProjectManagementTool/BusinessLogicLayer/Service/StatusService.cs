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
    public class StatusService : IStatusService
    {
        private readonly IStatusRepo _statusRepo;

        public StatusService(IStatusRepo statusRepo)
        {
            _statusRepo = statusRepo;
        }

        public void AddStatus(Status status)
        {
            _statusRepo.AddStatus(status);
        }

        public void DeleteStatus(int id)
        {
            _statusRepo.DeleteStatus(id);
        }

        public List<Status> GetAllStatuses()
        {
            var statuses = _statusRepo.GetAllStatuses();

            return statuses;
        }

        public Status GetStatusById(int id)
        {
            var status = _statusRepo.GetStatusById(id);

            return status;
        }

        public void UpdateStatus(Status status)
        {
            _statusRepo.UpdateStatus(status);
        }
    }
}
