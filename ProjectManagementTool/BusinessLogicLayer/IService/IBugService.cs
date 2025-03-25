using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace BusinessLogicLayer.IService
{
    public interface IBugService
    {
        public void AddBug(Bug bug);
        public void UpdateBug(Bug bug);
        public void DeleteBug(Bug bug);
        public Bug GetBug(int id);
        public List<Bug> GetAllBugOfStory(int id);
    }
}
