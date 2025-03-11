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

        public List<RoleVM> GetAllRole()
        {
            try
            {
                return _roleRepo.GetAllRole();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RoleVM GetRoleById(int id)
        {
            try
            {
                return _roleRepo.GetRoleById(id);
            }
            catch(Exception)
            {
                throw;
            }
        } 
    }
}
