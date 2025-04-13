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
    public class ProjectInfoRepo: IProjectInfoRepo
    {
        private readonly PMSDBContext _context;
        public ProjectInfoRepo( PMSDBContext context)
        {
            _context = context;

        }

        public bool AddProjectInfo(ProjectInfo projectInfo)
        {
            try
            {
                var existProject = _context.ProjectInfo.FirstOrDefault(p => p.Name == projectInfo.Name);
                if (existProject == null)
                {
                    _context.ProjectInfo.Add(projectInfo);
                    _context.SaveChanges();
                   
                    return true;
                }
                else
                {
                    return false;
                }   
            }
            catch (Exception  )
            {
                throw;
            }
            
        }

        public bool DeleteProjectInfo(ProjectInfo projectInfo)
        {
            try
            {
                _context.ProjectInfo.Remove(projectInfo);
                _context.SaveChanges();
                
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public ProjectInfo GetProjectInfo(int id)
        {
            try
            {
                var project = _context.ProjectInfo.Find(id);
                if (project == null)
                {
                    throw new Exception("Project not found!");
                }
                
                return project;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProjectInfo GetProjectInfo(string projectName)
        {
            try
            {
                var project = _context.ProjectInfo.FirstOrDefault( p => p.Name == projectName);
                if (project == null)
                {
                    throw new Exception("Project not found!");
                }
                return project;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ProjectInfo> GetAllProjectInfo()
        {
            try
            {
                var projects = _context.ProjectInfo.ToList();
                return projects;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ProjectInfo GetProjectInfoByName(int projectId, string name)
        {

            try
            {
                var project = _context.ProjectInfo.FirstOrDefault(p => p.Name == name && p.ProjectId != projectId);
                
                return project;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateProjectInfo(ProjectInfo projectInfo)
        {
            try
            {
                _context.ProjectInfo.Update(projectInfo);
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
