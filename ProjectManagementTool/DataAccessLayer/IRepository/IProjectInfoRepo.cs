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
        public bool AddProjectInfo(ProjectInfo projectInfo);
        public bool UpdateProjectInfo(ProjectInfo projectInfo);
        public bool DeleteProjectInfo(ProjectInfo projectInfo);
        public ProjectInfo GetProjectInfo(int id);
        public ProjectInfo GetProjectInfo(string projectName);
        public List<ProjectInfo> GetAllProjectInfo();
        public ProjectInfo GetProjectInfoByName(int projectId, string name);
    }
}
