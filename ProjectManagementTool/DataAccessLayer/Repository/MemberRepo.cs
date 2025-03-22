using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class MemberRepo : IMemberRepo
    {
        private readonly PMSDBContext _context;
        public MemberRepo(PMSDBContext context)
        {
            _context = context;
        }

        public void AddMember(Member member)
        {
            try
            {
                var existUser = _context.Members.FirstOrDefault(m => m.Email == member.Email && m.ProjectId == member.ProjectId);

                if (existUser == null)
                {
                    _context.Members.Add(member);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("The member already exists.");

                }

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while adding member.", ex);
            }
        }

        public void DeleteMember(Member member)
        {
            try
            {
                _context.Members.Remove(member);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while deleting member.", ex);
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
                    ProjectId = m.ProjectId

                    
                }).ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while listing all member.", ex);
            }
        }

        public Member GetMember(int id)
        {
            try
            {
                var member = _context.Members.Find(id);
                return member;

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while getting member.", ex);
            }
        }

        public void UpdateMember(Member member)
        {
            try
            {
                _context.Members.Update(member);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating member.", ex);
            }
        }

        public UserInfo GetUserByEmail(string email)
        {
            try
            {
                var user = _context.Users.FirstOrDefault( u => u.Email == email);
                return user;

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while finding user.", ex);
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
                        Projects = p != null ? ( _context.ProjectInfo.Where( z => z.ProjectId == x.f.ProjectId).Select( d => d.Key).ToList() ) : new List<string>()
                    }).ToList();

                var result2 = result.GroupBy(x => x.Id).Select(x => new AllUserVM
                {
                    Id = x.Key,
                    Pin = x.Select(y => y.Pin).FirstOrDefault(),
                    Name = x.Select(y => y.Name).FirstOrDefault(),
                    Email = x.Select(y => y.Email).FirstOrDefault(),
                    ProjectId = x.Select(y => y.ProjectId).FirstOrDefault(),
                    Projects = x.SelectMany(y => y.Projects).ToList()
                }).ToList();
                return result2;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
