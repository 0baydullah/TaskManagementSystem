using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface ISprintService
    {
        public void AddSprint(Sprint sprint);
        public void UpdateSprint(Sprint sprint);
        public void DeleteSprint(Sprint sprint);
        public Sprint GetSprint(int id);
        public List<Sprint> GetAllSprint();
    }
}
