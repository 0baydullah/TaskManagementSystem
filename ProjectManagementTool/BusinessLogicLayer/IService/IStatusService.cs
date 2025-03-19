using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace BusinessLogicLayer.IService
{
    public interface IStatusService
    {
        List<Status> GetAllStatuses();
        Status GetStatusById(int id);
        void AddStatus(Status status);
        void UpdateStatus(Status status);
        void DeleteStatus(int id);
    }
}
