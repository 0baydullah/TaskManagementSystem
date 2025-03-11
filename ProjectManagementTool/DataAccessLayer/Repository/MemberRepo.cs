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
                _context.Members.Add(member);
                _context.SaveChanges();
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
                //var roles = _context.Roles.Select(x => new RolesVM { RoleId = x.Id, RoleName = x.Name}).ToList();
                //var result = _context.Members.Join(_context.Users, member => member.Email, user => user.Email, (member, user) => 
                //new MemberWithRoleVM {
                //    Name = user.Name,
                //    Email = member.Email,
                //    Roles = roles,

                //}).ToList();
                //return result;

               // var x = _context.Users.ToList();

                var result = _context.Members.Join(_context.Users, member => member.Email, user => user.Email, (member, user) =>
                new 
                {
                    MemberId = member.MemberId,
                    Name = user.Name,
                    Email = member.Email,
                    RoleId = member.RoleId,

                }).Join(_context.Roles, m => m.RoleId, r => r.Id, (m, r) => new MemberWithRoleVM
                {
                    MemberId = m.MemberId,
                    Name = m.Name,
                    Email = m.Email,
                    Role = r.Name
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
    }
}
