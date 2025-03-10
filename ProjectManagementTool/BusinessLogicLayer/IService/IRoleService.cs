using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IRoleService
    {
        public Task<bool> AddRole(RoleVM role);
        public RoleVM GetRoleById(int id);
        public List<RoleVM> GetAllRole();
        public bool UpdateRole(RoleVM role);
        public bool DeleteRole(int id);
    }
}
