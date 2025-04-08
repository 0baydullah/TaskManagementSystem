using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepo _memberRepo;
        private readonly ITasksRepo _tasksRepo;
        private readonly IUserStoryRepo _userStoryRepo;
        private readonly IProjectInfoRepo _projectInfoRepo;
        public MemberService(IMemberRepo memberRepo, ITasksRepo tasksRepo, IUserStoryRepo userStoryRepo, IProjectInfoRepo projectInfoRepo)
        {
            _memberRepo = memberRepo;
            _tasksRepo = tasksRepo;
            _userStoryRepo = userStoryRepo;
            _projectInfoRepo = projectInfoRepo;
        }
        public bool AddMember(Member member)
        {
            try
            {
               var result = _memberRepo.AddMember(member);
               
               return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteMember(Member member)
        {
            try
            {
               var result = _memberRepo.DeleteMember(member);

               return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MemberWithRoleVM> GetAllMember()
        {
            try
            {
                return _memberRepo.GetAllMember();
            }
            catch (Exception)
            {
                throw;
            }  
        }

        public Member GetMember(int id)
        {
            try
            {
                var member = _memberRepo.GetMember(id);
               
                return member;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateMember(int id, MemberVM member)
        {
            try
            {
                var data = _memberRepo.GetMember(id);
                data.Email = member.Email;
                data.RoleId = member.RoleId;
                data.ProjectId = member.ProjectId;
                var result = await _memberRepo.UpdateMember(data);
                
                return result;
            }
            catch (Exception)
            {
                throw;
            }   
        }

        public UserInfo GetUserByEmail(string email)
        {
            try
            {
                var user = _memberRepo.GetUserByEmail(email);
                
                return user;
            }
            catch (Exception)
            {
                throw;
            }
            
        }


        public List<AllUserVM> GetAllUser(List<UserInfo> user)
        {
            try
            {
                var users = _memberRepo.GetAllUser(user);
                
                return users;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public MemberDetailsVM GetMemberDetails(int id)
        {
            try
            {
                var member = _memberRepo.GetAllMember().Where( m => m.MemberId == id).ToList();
                var projects = _projectInfoRepo.GetAllProjectInfo();
                var storys = _userStoryRepo.GetAllUserStory().ToList();
                var tasks = _tasksRepo.GetAllTasks();
                var allTasks = tasks.Join(storys, task => task.UserStoryId, story => story.StoryId, (task, story) => new
                {
                    TaskId = task.TaskId,
                    TaskName = task.Name,
                    Description = task.Descripton,
                    AssignMemberId = task.AssignMembersId,
                    ReviewerMemberId = task.ReviewerMemberId,
                    EstimatedTime = task.EstimatedTime,
                    Tag = task.Tag,
                    Status = task.Status,
                    Priority = task.Priority,
                    StoryId = story.StoryId,
                    StoryName = story.StoryName,
                    ProjectId = story.ProjectId,

                }).Join(member, t => t.ProjectId, member => member.ProjectId, (t, member) => new TasksVM
                {
                    TaskId = t.TaskId,
                    Name = t.TaskName,
                    Descripton = t.Description,
                    AssignMembersId = t.AssignMemberId,
                    ReviewerMemberId = t.ReviewerMemberId,
                    EstimatedTime = t.EstimatedTime,
                    Tag = t.Tag,
                    Status = t.Status,
                    Priority = t.Priority,
                    UserStoryId = t.StoryId,
                    //StoryName = t.StoryName,
                    //ProjectId = t.ProjectId,
                    //MemberId = member.MemberId,
                }).ToList();


                var result = new MemberDetailsVM
                {
                    MemberId = member.FirstOrDefault().MemberId,
                    MemberName = member.FirstOrDefault().Name,
                    RoleName = member.FirstOrDefault().Role,
                    Email = member.FirstOrDefault().Email,
                    Tasks = allTasks,
                    Bugs = null,
                };

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
