using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IReleaseRepo
    {
        public void AddRelease(Release release);
        public void UpdateRelease(Release release);
        public void DeleteRelease(Release release);
        public Release GetRelease(int id);
        public List<Release> GetAllReleases();
    }
}
