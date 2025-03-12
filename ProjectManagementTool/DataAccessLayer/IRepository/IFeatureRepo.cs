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
        public Task<bool> Create(Feature feature);
        public Task<List<Feature>> GetAllFeature();
        public Task<Feature> GetFeatureById(int id);
        public Task<bool> Update(int id);
        public Task<bool> Delete(int id);
    }
}
