using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepo _roleRepo;

        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }
        public async Task<bool> AddRole(RoleVM role)
        {
            throw new NotImplementedException();
        }

        public List<RoleVM> GetAllRole()
        {
            return _roleRepo.GetAllRole();
        }

        public RoleVM GetRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRole(RoleVM role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRole(int id)
        {
            var result = _roleRepo.DeleteRole(id);
            return result;
        }
    }
}
