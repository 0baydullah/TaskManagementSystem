using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IReleaseService
    {
        public bool AddRelease(Release release);
        public bool UpdateRelease(Release release);
        public bool DeleteRelease(Release release);
        public Release GetRelease(int id);
        public List<Release> GetAllReleases();

    }
}
