using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace BusinessLogicLayer.IService
{
    public interface IBugStatusService
    {
        List<BugStatus> GetAllStatuses();
        BugStatus GetStatusById(int id);
        void AddStatus(BugStatus status);
        void UpdateStatus(BugStatus status);
        void DeleteStatus(int id);
    }
}
