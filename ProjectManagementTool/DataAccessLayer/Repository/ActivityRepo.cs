using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{

    public class ActivityRepo : IActivityRepo
    {

        private readonly PMSDBContext _context;
        public ActivityRepo( PMSDBContext context)
        {
            _context = context;
        }
        public bool Add(Activity activity)
        {
            try
            {
                if (activity != null)
                {
                    _context.Actvities.Add(activity);
                    _context.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<Activity> GetAllActivities(int projectId)
        {
            try
            {
                var data = _context.Actvities.ToList();

                return data;
            }
            catch (Exception)
            {

                throw;
            }
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

                _context.Actvities.Add(model);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
