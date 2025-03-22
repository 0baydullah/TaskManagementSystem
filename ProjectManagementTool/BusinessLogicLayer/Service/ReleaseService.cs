using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class ReleaseService : IReleaseService
    {
        private readonly IReleaseRepo _releaseRepo;
        public ReleaseService(IReleaseRepo releaseRepo)
        {
            _releaseRepo = releaseRepo; 
        }
        public void AddRelease(Release release)
        {
            try
            {
                _releaseRepo.AddRelease(release);

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void DeleteRelease(Release release)
        {
            _releaseRepo.DeleteRelease(release);
        }

        public List<Release> GetAllReleases()
        {
            var releases = _releaseRepo.GetAllReleases();
            return releases;
        }

        public Release GetRelease(int id)
        {
            var release = _releaseRepo.GetRelease(id);
            return release;
        }

        public void UpdateRelease(Release release)
        {
            _releaseRepo.UpdateRelease(release);
        }
    }
}
