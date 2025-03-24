using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ISprintRepo
    {
        public bool AddSprint(Sprint sprint);
        public Task<bool> UpdateSprint(Sprint sprint);
        public bool DeleteSprint(Sprint sprint);
        public Sprint GetSprint(int id);
        public Sprint GetSprintByName(int id, int releaseId, string name);
        public List<SprintVM> GetAllSprint(int projectId);

    }
}
