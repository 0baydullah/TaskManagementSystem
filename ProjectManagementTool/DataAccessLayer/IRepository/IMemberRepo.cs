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
        public void AddMember(Member member);
        public void UpdateMember(Member member);
        public void DeleteMember(Member member);
        public Member GetMember(int id);
        public List<MemberWithRoleVM> GetAllMember();
        public UserInfo GetUserByEmail(string email);
    }
}
