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
        public bool AddRelease(Release release)
        {
            try
            {
                var existRelease = _context.Releases.FirstOrDefault(r => r.ReleaseName == release.ReleaseName && r.ProjectId == release.ProjectId);
                if (existRelease == null)
                {
                    _context.Releases.Add(release);
                    _context.SaveChanges();
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteRelease(Release release)
        {
            try
            {
                _context.Releases.Remove(release);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Release> GetAllReleases()
        {
            try
            {
                var releases = _context.Releases.ToList();
               
                return releases;
            }
            catch (Exception)
            {
              throw;
            }
        }

        public Release GetRelease(int id)
        {
            try
            {
                var release = _context.Releases.Find(id);
                if (release == null)
                {
                    throw new Exception("Release not found!");
                }
     
                return release;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateRelease(Release release)
        {
            try
            {
                _context.Releases.Update(release);
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
