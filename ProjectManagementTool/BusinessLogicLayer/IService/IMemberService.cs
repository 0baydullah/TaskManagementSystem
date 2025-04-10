using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IMemberService
    {
        public bool AddMember(Member member);
        public Task<bool> UpdateMember(int id ,MemberVM member);

        public bool DeleteMember(Member member);

        public Member GetMember(int id);
        public List<Member> GetMemberByEmail(string email);

        public List<MemberWithRoleVM> GetAllMember();
        public List<AllUserVM> GetAllUser(List<UserInfo> user);

        public UserInfo GetUserByEmail(string email);
        public MemberDetailsVM GetMemberDetails(int id);

    }
}
