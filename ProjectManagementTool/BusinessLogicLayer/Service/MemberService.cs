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
        public void AddMember(Member member)
        {
            _memberRepo.AddMember(member);
        }

        public void DeleteMember(Member member)
        {
            _memberRepo.DeleteMember(member);
        }

        public List<MemberWithRoleVM> GetAllMember()
        {
            return _memberRepo.GetAllMember();
        }

        public Member GetMember(int id)
        {
            var member = _memberRepo.GetMember(id);
            return member;
        }

        public void UpdateMember(Member member)
        {
            var memberToUpdate = _memberRepo.GetMember(member.MemberId);

            if (memberToUpdate != null)
            {
                _memberRepo.UpdateMember(member);
            }
            else
            {
                throw new Exception("Member not found");
            }
        }

        public UserInfo GetUserByEmail(string email)
        {
            var user = _memberRepo.GetUserByEmail(email);
            return user;
        }
    }
}
