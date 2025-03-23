using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class ProjectInfoService : IProjectInfoService
    {
        private readonly IProjectInfoRepo _projectInfoRepo;
        private readonly IMemberRepo _memberRepo;
        private readonly IRoleRepo _roleRepo;
        public ProjectInfoService(IProjectInfoRepo projectInfoRepo, IMemberRepo memberRepo, IRoleRepo roleRepo)
        {
            _projectInfoRepo = projectInfoRepo;
            _memberRepo = memberRepo;
            _roleRepo = roleRepo;
        }


        public  bool AddProjectInfo(ProjectInfoVM model, UserInfo user)
        {
            try
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

                var result = _projectInfoRepo.AddProjectInfo(project);

                if (result == true)
                {
                    var projectId = _projectInfoRepo.GetProjectInfo(model.Name).ProjectId;
                    var role = _roleRepo.GetAllRole().FirstOrDefault(x => x.RoleName == "Admin");
                    var member = new Member
                    {
                        ProjectId = projectId,
                        RoleId = role.RoleId,
                        Email = user.Email,
                    };
                    _memberRepo.AddMember(member);

                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
           

            
        }

        public bool DeleteProjectInfo(ProjectInfo projectInfo)
        {
            try
            {
                _projectInfoRepo.DeleteProjectInfo(projectInfo);

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
                var projectInfo = _projectInfoRepo.GetProjectInfo(id);
                
                return projectInfo;
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
                var projectInfo = _projectInfoRepo.GetProjectInfo(projectName);
                
                return projectInfo;
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
                var projectInfo = _projectInfoRepo.GetAllProjectInfo();
                
                return projectInfo;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public bool UpdateProjectInfo(EditProjectInfoVM model)
        {
            try
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

    }
}