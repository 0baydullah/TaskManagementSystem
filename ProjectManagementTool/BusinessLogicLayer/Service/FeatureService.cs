using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;

namespace BusinessLogicLayer.Service
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepo _featureRepo;
        private readonly IActivityRepo _activityRepo;
        public FeatureService(IFeatureRepo featureRepo, IActivityRepo activityRepo)
        {
            _featureRepo = featureRepo;
            _activityRepo = activityRepo;
        }
        public async Task<bool> CreateFeature(FeatureVM featureVM, UserInfo user)
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

                if (result == true)
                {
                    _activityRepo.Add(featureVM.ProjectId, "Feature", featureVM.Name + " is created by ", user.Id);
                }

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

        public async Task<bool> UpdateFeature(int id, FeatureVM featureVM, UserInfo user)
        {
            try
            {
                var existFeature = await _featureRepo.GetFeatureById(id);
                var existFeatureName = await _featureRepo.GetFeatureByName(featureVM.Name, id, featureVM.ProjectId);
                var previousName = existFeature.Name;
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
                if (previousName == featureVM.Name)
                {
                    _activityRepo.Add(featureVM.ProjectId, "Feature", previousName + " is updated", user.Id);
                }
                else
                {
                    _activityRepo.Add(featureVM.ProjectId, "Feature", previousName + " is changed to " + featureVM.Name, user.Id);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteFeature(int id, UserInfo user)
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
                    _activityRepo.Add(feature.ProjectId, "Feature", feature.Name + " is deleted by ", user.Id);

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
