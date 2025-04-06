using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;

namespace BusinessLogicLayer.IService
{
    public interface IBugService
    {
        public void AddBug(int id, BugVM bug);
        public void UpdateBug(int id, BugVM bug);
        public void DeleteBug(Bug bug);
        public Bug GetBug(int id);
        public List<Bug> GetAllBugOfStory(int id);
    }
}
