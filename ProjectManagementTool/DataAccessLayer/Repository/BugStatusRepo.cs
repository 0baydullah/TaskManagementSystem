using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Repository
{
    public class BugStatusRepo : IBugStatusRepo
    {
        private readonly PMSDBContext _context;

        public BugStatusRepo(PMSDBContext context)
        {
            _context = context;
        }
        public void AddStatus(BugStatus status)
        {
            try
            {
                _context.BugStatuses.Add(status);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the status.", ex);
            }
        }

        public void DeleteStatus(int id)
        {
            try
            {
                var status = _context.BugStatuses.Find(id);
                if (status != null)
                {
                    _context.BugStatuses.Remove(status);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the status.", ex);
            }
        }

        public List<BugStatus> GetAllStatuses()
        {
            try 
            { 
                var statuses = _context.BugStatuses.ToList();

                return statuses;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the statuses.", ex);
            }
        }

        public BugStatus GetStatusById(int id)
        {
            try
            {
                var status = _context.BugStatuses.Find(id);

                return status;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the status.", ex);
            }
        }

        public void UpdateStatus(BugStatus status)
        {
            try
            {
                _context.BugStatuses.Update(status);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the status.", ex);
            }
        }
    }
}
