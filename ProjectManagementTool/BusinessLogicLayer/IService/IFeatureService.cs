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
        public Task<bool> Create(FeatureVM featureVM);
    }
}
