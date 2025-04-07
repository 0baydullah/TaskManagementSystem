using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
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
        private readonly ISprintRepo _sprintRepo;
        public ReleaseService(IReleaseRepo releaseRepo, ISprintRepo sprintRepo)
        {
            _releaseRepo = releaseRepo;
            _sprintRepo = sprintRepo;
        }
        public bool AddRelease(ReleaseCreateVM release)
        {
            try
            {
                var model = new Release
                {
                    ProjectId = release.ProjectId,
                    ReleaseName = release.ReleaseName,
                    Description = release.Description,
                    StartDate = release.StartDate,
                    EndDate = release.EndDate,
                };

                var result = _releaseRepo.AddRelease(model);

                return result;
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

        public ReleaseDetailsVM GetReleaseDetails(int id)
        {
            try
            {
                var release = _releaseRepo.GetRelease(id);
                var sprints = _sprintRepo.GetAllSprint(release.ProjectId);
                var releaseDetails = new ReleaseDetailsVM
                {
                    ReleaseId = release.ReleaseId,
                    ProjectId = release.ProjectId,
                    ReleaseName = release.ReleaseName,
                    Description = release.Description,
                    StartDate = release.StartDate,
                    EndDate = release.EndDate,
                    Sprints = sprints
                };
                return releaseDetails;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateRelease(int id, Release release)
        {
            try
            {
                var existRelease = _releaseRepo.GetRelease(id);
                var existReleaseName = _releaseRepo.GetReleaseByName(id, release.ProjectId, release.ReleaseName);

                if ( existRelease == null || existReleaseName != null)
                {
                    return false; 
                }

                existRelease.ReleaseName = release.ReleaseName;
                existRelease.Description = release.Description;
                existRelease.StartDate = release.StartDate;
                existRelease.EndDate = release.EndDate;
                existRelease.ProjectId = release.ProjectId;
                var result = await _releaseRepo.UpdateRelease(existRelease);
                
                return result;
            }
            catch (Exception)
            {
                throw;
            }         
        }
    }
}
