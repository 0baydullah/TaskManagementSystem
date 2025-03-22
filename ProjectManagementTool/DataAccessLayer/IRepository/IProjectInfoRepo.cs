using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IProjectInfoRepo
    {
        public void AddProjectInfo(ProjectInfo projectInfo);
        public void UpdateProjectInfo(ProjectInfo projectInfo);
        public void DeleteProjectInfo(ProjectInfo projectInfo);
        public ProjectInfo GetProjectInfo(int id);
        public ProjectInfo GetProjectInfo(string projectName);
        public List<ProjectInfo> GetAllProjectInfo();
    }
}
