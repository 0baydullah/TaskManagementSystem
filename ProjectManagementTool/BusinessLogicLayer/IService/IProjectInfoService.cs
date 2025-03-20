using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IProjectInfoService
    {
        public void AddProjectInfo(ProjectInfoVM projectInfo, UserInfo user);
        public void UpdateProjectInfo(EditProjectInfoVM projectInfo);

        public void DeleteProjectInfo(ProjectInfo projectInfo);

        public ProjectInfo GetProjectInfo(int id);

        public List<ProjectInfo> GetAllProjectInfo();

    }
}
