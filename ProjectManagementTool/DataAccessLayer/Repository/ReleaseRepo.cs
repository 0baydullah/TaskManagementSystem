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
    public class ReleaseRepo : IReleaseRepo
    {
        private readonly PMSDBContext _context;
        public ReleaseRepo(PMSDBContext context)
        {
            _context = context;
        }
        public void AddRelease(Release release)
        {
            try
            {
                _context.Releases.Add(release);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while adding the release!",ex);
            }
        }

        public void DeleteRelease(Release release)
        {
            try
            {
                _context.Releases.Remove(release);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while deleting the release!",ex);
            }
        }

        public List<Release> GetAllReleases()
        {
            try
            {
                var releases = _context.Releases.ToList();
                return releases;

            }
            catch (Exception ex)
            {
              throw new Exception("An error occurred while getting all releases!", ex);
            }
        }

        public Release GetRelease(int id)
        {
            try
            {
                var release = _context.Releases.Find(id);
                return release;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting release!", ex);
            }
        }

        public void UpdateRelease(Release release)
        {
            try
            {
                _context.Releases.Update(release);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the release!", ex);
            }
        }
    }
}
