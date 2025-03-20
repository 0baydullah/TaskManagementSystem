using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
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
        private readonly IFileService _fileService;
        public ProjectInfoService(IProjectInfoRepo projectInfoRepo, IFileService fileService)
        {
            _projectInfoRepo = projectInfoRepo;
            _fileService = fileService;
        }


        public  void AddProjectInfo(ProjectInfoVM model, UserInfo user)
        {

           

            var project = new ProjectInfo
            {
                Name = model.Name,
                Key = model.Key,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CompanyName = model.CompanyName,
                ProjectOwnerId = user.Id,
                
            };

            _projectInfoRepo.AddProjectInfo(project);
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

        public void UpdateProjectInfo(EditProjectInfoVM model)
        {
            var project = _projectInfoRepo.GetProjectInfo(model.ProjectId);
            if (project != null)
            {
                
                project.Name = model.Name;
                project.Key = model.Key;
                project.Description = model.Description;
                project.StartDate = model.StartDate;
                project.EndDate = model.EndDate;
                project.CompanyName = model.CompanyName;
                project.ProjectOwnerId = model.ProjectOwnerId;
         
                _projectInfoRepo.UpdateProjectInfo(project);

            }
            else
            {
                throw new Exception("Project not found");
            }
        }

    }
}