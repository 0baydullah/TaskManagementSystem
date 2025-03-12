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
        public async Task<bool> Create(FeatureVM featureVM)
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
                    Tag = featureVM.Tag,
                };

                var result = await _featureRepo.Create(feature);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
