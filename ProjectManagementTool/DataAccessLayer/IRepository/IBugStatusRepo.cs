using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.IRepository
{
    public interface IBugStatusRepo
    {
        List<BugStatus> GetAllStatuses();
        BugStatus GetStatusById(int id);
        void AddStatus(BugStatus status);
        void UpdateStatus(BugStatus status);
        void DeleteStatus(int id);
    }
}
