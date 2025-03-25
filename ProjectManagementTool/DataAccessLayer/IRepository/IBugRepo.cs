using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;

namespace DataAccessLayer.IRepository
{
    public interface IBugRepo
    {
        public void AddBug(Bug bug);
        public void UpdateBug(Bug bug);
        public void DeleteBug(Bug bug);
        public Bug GetBug(int id);
        public List<Bug> GetAllBugOfStory(int id);
    }
}
