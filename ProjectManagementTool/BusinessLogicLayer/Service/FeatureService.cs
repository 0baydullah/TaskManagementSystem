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
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepo _featureRepo;
        public FeatureService(IFeatureRepo featureRepo)
        {
            _featureRepo = featureRepo;
        }
        public async Task<bool> CreateFeature(FeatureVM featureVM) 
        {
            try
            {
                var feature = new Feature()
                {
                    Name = featureVM.Name,
                    Description = featureVM.Description,
                    EstimatedPoint = featureVM.EstimatedPoint,
                    ReleaseId = featureVM.ReleaseId,
                    MemberId = featureVM.MemberId,
                    ProjectId = featureVM.ProjectId,
                    Tag = featureVM.Tag,
                };

                var result = await _featureRepo.CreateFeature(feature);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<FeatureWithMemberReleaseVM>> GetAllFeature(int projectId)
        {
            try
            {
                var features = _featureRepo.GetAllFeature(projectId);
                return features;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Feature> GetFeatureById(int id)
        {
            try
            {
                var feature = _featureRepo.GetFeatureById(id);
                return feature;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateFeature(int id, FeatureVM featureVM)
        {
            try
            {
                var existFeature = await _featureRepo.GetFeatureById(id);
                var existFeatureName = await _featureRepo.GetFeatureByName(featureVM.Name, id, featureVM.ProjectId);

                if (existFeature == null || existFeatureName != null)
                {
                    return false;
                }

                existFeature.Name = featureVM.Name;
                existFeature.Description = featureVM.Description;
                existFeature.EstimatedPoint = featureVM.EstimatedPoint;
                existFeature.ReleaseId = featureVM.ReleaseId;
                existFeature.MemberId = featureVM.MemberId;
                existFeature.ProjectId = featureVM.ProjectId;
                existFeature.Tag = featureVM.Tag;

                var result = await _featureRepo.UpdateFeature(existFeature);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteFeature(int id) 
        {
            try
            {
                var feature = await _featureRepo.GetFeatureById(id);

                if (feature == null)
                {
                    return false;
                }
                else
                {
                    var result = await _featureRepo.DeleteFeature(feature);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
