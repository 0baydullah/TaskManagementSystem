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

        public void AddProjectInfo(ProjectInfo projectInfo)
        {
            try
            {
                var existProject = _context.ProjectInfo.FirstOrDefault(p => p.Name == projectInfo.Name);
                if (existProject == null)
                {
                    _context.ProjectInfo.Add(projectInfo);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("The project already exists.");

                }   
            }
            catch (Exception ex )
            {

                throw new Exception("An error occurred while adding the project.", ex);
            }
            
        }

        public void DeleteProjectInfo(ProjectInfo projectInfo)
        {
            try
            {
                _context.ProjectInfo.Remove(projectInfo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while removing the project.", ex);
            }
            
        }

        public ProjectInfo GetProjectInfo(int id)
        {
            try
            {
                var project = _context.ProjectInfo.Find(id);
                return project;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while getting information the project.", ex);
            }
        }
        public List<ProjectInfo> GetAllProjectInfo()
        {
            try
            {
                var projects = _context.ProjectInfo.ToList();
                return projects;

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while getting information the project.", ex);
            }
        }
        public void UpdateProjectInfo(ProjectInfo projectInfo)
        {
            try
            {
                _context.ProjectInfo.Update(projectInfo);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while getting information the project.", ex);
            }
            
        }
    }
}
