using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IFeatureRepo
    {
        public Task<bool> CreateFeature(Feature feature);
        public Task<List<FeatureWithMemberReleaseVM>> GetAllFeature(int projectId); 
        public Task<Feature> GetFeatureById(int id);
        public Task<Feature> GetFeatureByName(string name, int id, int projectId);
        public Task<bool> UpdateFeature(Feature feature);
        public Task<bool> DeleteFeature(Feature feature); 
    }
}
