using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

    }
}
