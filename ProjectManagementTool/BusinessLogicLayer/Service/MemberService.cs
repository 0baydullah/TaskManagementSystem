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
        public MemberService(IMemberRepo memberRepo)
        {
            _memberRepo = memberRepo;
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
    }
}
