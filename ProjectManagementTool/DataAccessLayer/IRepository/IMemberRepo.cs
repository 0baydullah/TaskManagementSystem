using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IMemberRepo
    {
        public bool AddMember(Member member);
        public Task<bool> UpdateMember(Member member);
        public bool DeleteMember(Member member);
        public Member GetMember(int id);
        public List<MemberWithRoleVM> GetAllMember();
        public UserInfo GetUserByEmail(string email);
        public List<AllUserVM> GetAllUser(List<UserInfo> user);
    }
}
