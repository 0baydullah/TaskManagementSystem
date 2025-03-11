using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public List<RoleVM> GetAllRole()
        {
            try
            {
                var roles = _context.Roles.Select(x => new RoleVM { RoleId = x.Id, RoleName = x.Name }).ToList();
                return roles;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public RoleVM GetRoleById(int id)
        {
            try
            {
                var role = _context.Roles.Where(x => x.Id == id).Select(x => new RoleVM { RoleId = x.Id, RoleName = x.Name }).ToList();
                return role[0];
            }
            catch(Exception)
            {
                throw;
            }
        }  
    }
}
