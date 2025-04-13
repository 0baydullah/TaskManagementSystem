using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepo _memberRepo;
        private readonly ITasksRepo _tasksRepo;
        private readonly IUserStoryRepo _userStoryRepo;
        private readonly IProjectInfoRepo _projectInfoRepo;
        private readonly IRoleRepo _roleRepo;
        private readonly IBugRepo _bugRepo;
        private readonly IStatusRepo _statusRepo;
        private readonly IPriorityRepo _priorityRepo;
        public MemberService(IMemberRepo memberRepo, ITasksRepo tasksRepo, IUserStoryRepo userStoryRepo, IProjectInfoRepo projectInfoRepo, 
            IRoleRepo roleRepo, IBugRepo bugRepo, IStatusRepo statusRepo, IPriorityRepo priorityRepo)
        {
            _memberRepo = memberRepo;
            _tasksRepo = tasksRepo;
            _userStoryRepo = userStoryRepo;
            _projectInfoRepo = projectInfoRepo;
            _roleRepo = roleRepo;
            _bugRepo = bugRepo;
            _statusRepo = statusRepo;
            _priorityRepo = priorityRepo;
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
                var members = _memberRepo.GetAllMember().ToList();
                return members;
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

        public List<Member> GetMemberByEmail(string email)
        {
            try
            {
                var members = _memberRepo.GetMemberByEmail(email);

                return members;
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
                data.RoleId = member.RoleId;
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
                var member = _memberRepo.GetMember(id);
                var tasks = _tasksRepo.GetAllTasks().Where(t => t.AssignMembersId == member.MemberId).ToList();
                var bugs = _bugRepo.GetAllBug().Where(b => b.AssignMembersId == member.MemberId).ToList();
                var roles =  _roleRepo.GetAllRole().ToDictionary(r => r.RoleId, r => r.RoleName);
                var status = _statusRepo.GetAllStatuses().ToDictionary( s => s.StatusId, s => s.Name);
                var priority = _priorityRepo.GetAllPriorities().ToDictionary( p => p.PriorityId, p => p.Name);

                Dictionary<int, int> dictTask = tasks.GroupBy(task => task.Status).ToDictionary(group => group.Key, group => group.Count());
                Dictionary<int, int> dictBug = bugs.GroupBy(bug => bug.Status).ToDictionary(group => group.Key, group => group.Count());

                var result = new MemberDetailsVM
                {
                    MemberId = member.MemberId,
                    ProjectId = member.ProjectId,
                    RoleId = member.RoleId,
                    Email = member.Email ?? "No Email!",
                    Tasks = tasks,
                    Bugs = bugs,
                    RoleList = roles,
                    StatusList = status,
                    PriorityList = priority,
                    TaskAll = tasks.Count,
                    BugAll = bugs.Count,
                    TaskDict = dictTask,
                    BugDict = dictBug
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
