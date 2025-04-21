using System;
using DataAccessLayer.Models.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IActivityRepo
    {
        public bool Add(Activity activity);
        public bool Add(int projectId, string root, string message, int performBy);
        public List<Activity> GetAllActivities(int projectId);
    }
}
