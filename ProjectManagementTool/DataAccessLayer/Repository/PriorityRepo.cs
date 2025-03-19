using DataAccessLayer.Data;
using DataAccessLayer.Enums;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Repository
{
    public class PriorityRepo : IPriorityRepo
    {
        private readonly PMSDBContext _context;

        public PriorityRepo(PMSDBContext context)
        {
            _context = context;
        }
        public void AddPriority(Priority priority)
        {
            try
            {
                _context.Priorities.Add(priority);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the Priority.", ex);
            }
        }

        public void DeletePriority(int id)
        {
            try
            {
                var priority = _context.Priorities.Find(id);
                if (priority != null)
                {
                    _context.Priorities.Remove(priority);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the Priority.", ex);
            }
        }

        public List<Priority> GetAllPriorities()
        {
            try 
            { 
                var priorities = _context.Priorities.ToList();

                return priorities;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the priorities.", ex);
            }
        }

        public Priority GetPriorityById(int id)
        {
            try
            {
                var priority = _context.Priorities.Find(id);

                return priority;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the priorities.", ex);
            }
        }

        public void UpdatePriority(Priority priority)
        {
            try
            {
                _context.Priorities.Update(priority);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the priority.", ex);
            }
        }
    }
}
