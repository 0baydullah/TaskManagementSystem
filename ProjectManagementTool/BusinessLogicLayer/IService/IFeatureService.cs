using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IFeatureService
    {
        public Task<bool> CreateFeature(FeatureVM featureVM, UserInfo user);
        public Task<List<FeatureWithMemberReleaseVM>> GetAllFeature(int projectId);
        public Task<Feature> GetFeatureById(int id);
        public Task<bool> UpdateFeature(int id, FeatureVM featureVM, UserInfo user);
        public Task<bool> DeleteFeature(int id, UserInfo user);
    }
}
