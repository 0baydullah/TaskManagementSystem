using DataAccessLayer.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IRoleRepo
    {
        public RoleVM GetRoleById(int id);
        public List<RoleVM> GetAllRole(); 
    }
}
