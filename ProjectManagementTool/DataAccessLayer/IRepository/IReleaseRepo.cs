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
        public bool AddRelease(Release release);
        public Task<bool> UpdateRelease(Release release);
        public bool DeleteRelease(Release release);
        public Release GetRelease(int id);
        public Release GetReleaseByName(int id, int projectId, string name);
        public List<Release> GetAllReleases();
    }
}
