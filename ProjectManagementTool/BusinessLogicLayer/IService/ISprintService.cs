using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface ISprintService
    {
        public bool AddSprint(SprintCreateVM sprint);
        public Task<bool> UpdateSprint(int id, SprintCreateVM sprint);
        public bool DeleteSprint(Sprint sprint);
        public Sprint GetSprint(int id);
        public List<SprintVM> GetAllSprint(int projectId);

        public SprintDetailsVM GetSprintDetails(int id);

    }
}
