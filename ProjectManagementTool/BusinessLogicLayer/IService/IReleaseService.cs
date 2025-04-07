using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IReleaseService
    {
        public bool AddRelease(ReleaseCreateVM release);
        public Task<bool> UpdateRelease(int id, Release release);
        public bool DeleteRelease(Release release);
        public Release GetRelease(int id);
        public List<Release> GetAllReleases();
        public ReleaseDetailsVM GetReleaseDetails(int id);
    }
}
