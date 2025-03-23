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
        public bool AddRelease(Release release)
        {
            try
            {
                var result = _releaseRepo.AddRelease(release);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public bool DeleteRelease(Release release)
        {
            try
            {
                var result = _releaseRepo.DeleteRelease(release);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public List<Release> GetAllReleases()
        {
            try
            {
                var releases = _releaseRepo.GetAllReleases();
                
                return releases;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public Release GetRelease(int id)
        {
            try
            {
                var release = _releaseRepo.GetRelease(id);
               
                return release;

            }
            catch (Exception)
            {
                throw;
            }     
        }

        public bool UpdateRelease(Release release)
        {
            try
            {
                var result = _releaseRepo.UpdateRelease(release);
                
                return result;
            }
            catch (Exception)
            {
                throw;
            }         
        }
    }
}
