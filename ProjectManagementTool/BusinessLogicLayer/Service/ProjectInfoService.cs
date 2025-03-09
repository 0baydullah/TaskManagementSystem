using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class ProjectInfoService : IProjectInfoService
    {
        private readonly IProjectInfoRepo _projectInfoRepo;
        public ProjectInfoService(IProjectInfoRepo projectInfoRepo)
        {
            _projectInfoRepo = projectInfoRepo;

        }


        public void AddProjectInfo(ProjectInfo projectInfo)
        {
            _projectInfoRepo.AddProjectInfo(projectInfo);
        }

        public void DeleteProjectInfo(ProjectInfo projectInfo)
        {
            _projectInfoRepo.DeleteProjectInfo(projectInfo);
        }
        public ProjectInfo GetProjectInfo(int id)
        {
            var projectInfo = _projectInfoRepo.GetProjectInfo(id);
            return projectInfo;
        }
        public List<ProjectInfo> GetAllProjectInfo()
        {
            var projectInfo = _projectInfoRepo.GetAllProjectInfo();
            return projectInfo;
        }

        public void UpdateProjectInfo(ProjectInfo projectInfo)
        {
            var project = _projectInfoRepo.GetProjectInfo(projectInfo.ProjectId);
            if (project != null)
            {
                _projectInfoRepo.UpdateProjectInfo(projectInfo);

            }
            else
            {
                throw new Exception("Project not found");
            }
        }
    }
}