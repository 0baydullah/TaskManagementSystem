using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IProjectInfoService
    {
        public void AddProjectInfo(ProjectInfo projectInfo);
        public void UpdateProjectInfo(ProjectInfo projectInfo);

        public void DeleteProjectInfo(ProjectInfo projectInfo);

        public ProjectInfo GetProjectInfo(int id);

        public List<ProjectInfo> GetAllProjectInfo();

    }
}
