using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        private readonly IReleaseRepo _releaseRepo;
        private readonly ISprintRepo _sprintRepo;
        private readonly IUserStoryRepo _userStoryRepo;
        private readonly IFeatureRepo _featureRepo;
        private readonly ITasksRepo _tasksRepo;
        private readonly IBugRepo _bugRepo;

        public ProjectInfoService(IProjectInfoRepo projectInfoRepo, IMemberRepo memberRepo, IRoleRepo roleRepo, IReleaseRepo releaseRepo, 
            ISprintRepo sprintRepo, IUserStoryRepo userStoryRepo, IFeatureRepo featureRepo, ITasksRepo tasksRepo, IBugRepo bugRepo)
        {
            _projectInfoRepo = projectInfoRepo;
            _memberRepo = memberRepo;
            _roleRepo = roleRepo;
            _releaseRepo = releaseRepo;
            _sprintRepo = sprintRepo;
            _userStoryRepo = userStoryRepo;
            _featureRepo = featureRepo;
            _tasksRepo = tasksRepo;
            _bugRepo = bugRepo;
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
                    var role = _roleRepo.GetAllRole().FirstOrDefault(x => x.RoleName == "Owner");
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
        public List<ProjectInfo> GetAllProjectInfo(string email)
        {
            try
            {
                var member = _memberRepo.GetAllMember().Where(x => x.Email == email).ToList();
                var projectInfo = _projectInfoRepo.GetAllProjectInfo();
                var memberProject = projectInfo.Join(member, p => p.ProjectId, m => m.ProjectId, (p, m) => new ProjectInfo
                { 
                    ProjectId = p.ProjectId,
                    Name = p.Name,
                    Key = p.Key,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    CompanyName = p.CompanyName,
                    ProjectOwnerId = p.ProjectOwnerId,

                }).ToList();
               
                return memberProject;
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
                var existProject = _projectInfoRepo.GetProjectInfo(model.ProjectId);
                var existProjectName = _projectInfoRepo.GetProjectInfoByName(model.ProjectId, model.Name);
                
                if (existProject == null || existProjectName != null)
                {
                    return false;
                }

                existProject.Name = model.Name;
                existProject.Key = model.Key;
                existProject.Description = model.Description;
                existProject.StartDate = model.StartDate;
                existProject.EndDate = model.EndDate;
                existProject.CompanyName = model.CompanyName;
                existProject.ProjectOwnerId = model.ProjectOwnerId;
                _projectInfoRepo.UpdateProjectInfo(existProject);

                return true;
            }
            catch (Exception)
            {
                throw;
            }    
        }

        public async Task<ProjectDetailsVM> GetProjectInfoDetails(int id)
        {
            try
            {
                var projectInfo = _projectInfoRepo.GetProjectInfo(id);
                var member = _memberRepo.GetAllMember().Where(x => x.ProjectId == id).Count();
                var release = _releaseRepo.GetAllReleases().Where(x => x.ProjectId == id).Count();
                var sprint = _sprintRepo.GetAllSprint(id).Count;
                var feature = await _featureRepo.GetAllFeature(id);

                var userStory = _userStoryRepo.GetAllUserStory().Where( u => u.ProjectId == id);
                var tasks = _tasksRepo.GetAllTasks();
                var bugs = _bugRepo.GetAllBug();
                var allTasks = userStory.Join(tasks, u => u.StoryId, t => t.UserStoryId, (u, t) => new
                {
                    u.StoryId,
                    t.Id,
                }).ToList().Count;

                var allBugs = userStory.Join(bugs, u => u.StoryId, b => b.UserStoryId, (u, b) => new
                {
                    u.StoryId,
                    b.BugId,
                }).ToList().Count;


                var projectDetails = new ProjectDetailsVM
                {
                    ProjectId = projectInfo.ProjectId,
                    ProjectName = projectInfo.Name,
                    Key = projectInfo.Key,
                    Member = member,
                    Release = release,
                    Task = allTasks,
                    Bug = allBugs,
                    UserStory = userStory.Count(),
                    Feature = feature.Count,
                    Sprint = sprint,
                };
                return projectDetails;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Tasks> GetAllTaskByProject(int projectId)
        {
            try
            {
                var story = _userStoryRepo.GetAllUserStory().Where( u => u.ProjectId == projectId).ToList();
                var tasks = _tasksRepo.GetAllTasks().ToList();
                var result = tasks.Join(story, t => t.UserStoryId, u => u.StoryId, (t, u) => new Tasks
                {
                    Id = t.Id,
                    Name = t.Name,
                    Descripton = t.Descripton,
                    AssignMembersId = t.AssignMembersId,
                    ReviewerMemberId = t.ReviewerMemberId,
                    EstimatedTime = t.EstimatedTime,
                    Tag = t.Tag,
                    Status = t.Status,
                    Priority = t.Priority,
                    UserStoryId = u.StoryId,
                }).ToList();

                return result;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Bug> GetAllBugByProject(int projectId)
        {
            try
            {
                var story = _userStoryRepo.GetAllUserStory().Where(u => u.ProjectId == projectId).ToList();
                var bugs = _bugRepo.GetAllBug();
                var allBugs = story.Join(bugs, u => u.StoryId, b => b.UserStoryId, (u, b) => new Bug
                {
                    BugId = b.BugId,
                    Name = b.Name,
                    Descripton = b.Descripton,
                    AssignMembersId = b.AssignMembersId,
                    QaRemarks = b.QaRemarks,
                    Status = b.Status,
                    Priority = b.Priority,
                    UserStoryId = u.StoryId

                }).ToList();

                return allBugs;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool UpdateProjectOwner(int ownerId, int projectId)
        {
            try
            {
                //owner update on project
                var project = _projectInfoRepo.GetProjectInfo(projectId);
                project.ProjectOwnerId = ownerId;
                _projectInfoRepo.UpdateProjectInfo(project);

                //owner update on member 
                var member = _memberRepo.GetMember(ownerId);
                var role = _roleRepo.GetAllRole().FirstOrDefault(x => x.RoleName == "Owner");
                member.RoleId = role.RoleId;
                _memberRepo.UpdateMember(member);
                
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}