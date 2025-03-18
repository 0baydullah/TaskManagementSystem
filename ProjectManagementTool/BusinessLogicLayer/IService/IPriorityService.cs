using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace BusinessLogicLayer.IService
{
    public interface IPriorityService
    {
        List<Priority> GetAllPriority();
        Priority GetPriorityById(int id);
        void AddPriority(Priority priority);
        void UpdatePriority(Priority priority);
        void DeletePriority(int id);
    }
}
