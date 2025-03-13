using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ISprintRepo
    {
        public void AddSprint(Sprint sprint);
        public void UpdateSprint(Sprint sprint);
        public void DeleteSprint(Sprint sprint);
        public Sprint GetSprint(int id);
        public List<Sprint> GetAllSprint();

    }
}
