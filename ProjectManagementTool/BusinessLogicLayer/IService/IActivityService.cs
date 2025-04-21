using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IActivityService
    {
        public bool Add(int projectId, string root, string message, int performBy);
        public List<Activity> GetAllActivities(int projectId);

    }
}
