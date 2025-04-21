using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepo _activityRepo;
        public ActivityService(IActivityRepo activityRepo)
        {
           _activityRepo = activityRepo;
        }
        public bool Add(int projectId, string root, string message, int performBy)
        {
            try
            {
                var model = new Activity
                { 
                    ProjectId = projectId,
                    Root = root,
                    Message = message,
                    PerformBy = performBy,
                    PerformAt = DateTime.Now,
                };

                var result = _activityRepo.Add(model);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public  List<Activity> GetAllActivities(int projectId)
        {
            try
            {
                var data = _activityRepo.GetAllActivities(projectId);
                
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
