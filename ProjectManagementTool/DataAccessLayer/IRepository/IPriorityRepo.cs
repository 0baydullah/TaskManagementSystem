using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.IRepository
{
    public interface IPriorityRepo
    {
        List<Status> GetAllPriorities();
        Status GetPriorityById(int id);
        void AddPriority(Status status);
        void UpdatePriority(Status status);
        void DeletePriority(int id);
    }
}
