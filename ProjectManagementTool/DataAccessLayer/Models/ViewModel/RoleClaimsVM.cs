using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class RoleClaimsVM
    {
        public RoleClaimsVM()
        {
            Claims = new List<RoleClaim>();
        }

        public int RoleId { get; set; } 
        public List<RoleClaim> Claims { get; set; }
    }
}
