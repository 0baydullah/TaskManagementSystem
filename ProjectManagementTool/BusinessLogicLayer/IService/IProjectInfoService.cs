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
        public bool AddProjectInfo(ProjectInfoVM projectInfo, UserInfo user);
        public bool UpdateProjectInfo(EditProjectInfoVM projectInfo);

        public bool DeleteProjectInfo(ProjectInfo projectInfo);

        public ProjectInfo GetProjectInfo(int id);

        public ProjectInfo GetProjectInfo(string projectName);

        public Task<ProjectDetailsVM> GetProjectInfoDetails(int id);

        public List<ProjectInfo> GetAllProjectInfo();

        public List<ProjectInfo> GetAllProjectInfo(string email);
        public List<Tasks> GetAllTaskByProject(int projectId);
        public List<Bug> GetAllBugByProject(int projectId);

        public bool UpdateProjectOwner(int ownerId, int projectId);

    }
}
