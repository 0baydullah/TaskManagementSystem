using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class RoleRepo : IRoleRepo
    {
        private readonly PMSDBContext _context;

        public RoleRepo(PMSDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRole(RoleVM role)
        {
            throw new NotImplementedException();
        }

        public List<RoleVM> GetAllRole()
        {
            var roles = _context.Roles.Select(x => new RoleVM { RoleId = x.Id, RoleName = x.Name }).ToList();
            return roles;
        }

        public RoleVM GetRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRole(RoleVM role)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRole(int id)
        {
            throw new NotImplementedException();
        }
    }
}
