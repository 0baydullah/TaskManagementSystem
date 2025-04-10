using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;

namespace DataAccessLayer.Repository
{
    public class MemberRepo : IMemberRepo
    {
        private readonly PMSDBContext _context;
        public MemberRepo(PMSDBContext context)
        {
            _context = context;
        }

        public bool AddMember(Member member)
        {
            try
            {
                var existUser = _context.Members.FirstOrDefault(m => m.Email == member.Email && m.ProjectId == member.ProjectId);

                if (existUser == null)
                {
                    _context.Members.Add(member);
                    _context.SaveChanges();

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

        public bool DeleteMember(Member member)
        {
            try
            {
                _context.Members.Remove(member);
                _context.SaveChanges();

                return true;
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
                var result = _context.Members.Join(_context.Users, member => member.Email, user => user.Email, (member, user) =>
                new
                {
                    MemberId = member.MemberId,
                    Name = user.Name,
                    Email = member.Email,
                    RoleId = member.RoleId,
                    ProjectId = member.ProjectId

                }).Join(_context.Roles, m => m.RoleId, r => r.Id, (m, r) => new MemberWithRoleVM
                {
                    MemberId = m.MemberId,
                    Name = m.Name,
                    Email = m.Email,
                    Role = r.Name,
                    ProjectId = m.ProjectId,
                    TotalTask = _context.Tasks.Where( t => t.AssignMembersId == m.MemberId).Count(),
                    TotalBug = _context.Bugs.Where(b => b.AssignMembersId == m.MemberId).Count(),

                }).ToList();

                return result;
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
                var member = _context.Members.Find(id);
                if (member == null)
                {
                    throw new Exception("Member not found!");
                }

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
                var members = _context.Members.Where(x => x.Email == email).ToList();
                if (members == null)
                {
                    throw new Exception("Member not found!");
                }

                return members;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateMember(Member member)
        {
            try
            {
                _context.Members.Update(member);
                await _context.SaveChangesAsync();

                return true;
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
                var user = _context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    throw new Exception("User not found!");
                }

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
                var result = user.GroupJoin(_context.Members, u => u.Email, m => m.Email, (u, m) => new { u, m }).SelectMany(
                    x => x.m.DefaultIfEmpty(), (x, q) => new
                    {
                        UserId = x.u.Id,
                        Pin = x.u.Pin,
                        Name = x.u.Name,
                        Email = x.u.Email,
                        ProjectId = q != null ? q.ProjectId : 0
                    }).
                GroupJoin(_context.ProjectInfo, f => f.ProjectId, p => p.ProjectId, (f, p) => new { f, p }).SelectMany(
                    x => x.p.DefaultIfEmpty(), (x, p) => new AllUserVM
                    {
                        Id = x.f.UserId,
                        Pin = x.f.Pin,
                        Name = x.f.Name,
                        Email = x.f.Email,
                        ProjectId = x.f.ProjectId,
                        Projects = p != null ? (_context.ProjectInfo.Where(z => z.ProjectId == x.f.ProjectId).Select(d => d.Key).ToList()) : new List<string>()
                    }).ToList();

                var allUser = result.GroupBy(x => x.Id).Select(x => new AllUserVM
                {
                    Id = x.Key,
                    Pin = x.Select(y => y.Pin).FirstOrDefault(),
                    Name = x.Select(y => y.Name).FirstOrDefault(),
                    Email = x.Select(y => y.Email).FirstOrDefault(),
                    ProjectId = x.Select(y => y.ProjectId).FirstOrDefault(),
                    Projects = x.SelectMany(y => y.Projects).ToList()
                }).ToList();

                return allUser;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
