using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Repository
{
    public class StatusRepo : IStatusRepo
    {
        private readonly PMSDBContext _context;

        public StatusRepo(PMSDBContext context)
        {
            _context = context;
        }
        public void AddStatus(Status status)
        {
            try
            {
                _context.Statuses.Add(status);
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
                var status = _context.Statuses.Find(id);
                if (status != null)
                {
                    _context.Statuses.Remove(status);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the status.", ex);
            }
        }

        public List<Status> GetAllStatuses()
        {
            try 
            { 
                var statuses = _context.Statuses.ToList();

                return statuses;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the statuses.", ex);
            }
        }

        public Status GetStatusById(int id)
        {
            try
            {
                var status = _context.Statuses.Find(id);

                return status;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the status.", ex);
            }
        }

        public void UpdateStatus(Status status)
        {
            try
            {
                _context.Statuses.Update(status);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the status.", ex);
            }
        }
    }
}
