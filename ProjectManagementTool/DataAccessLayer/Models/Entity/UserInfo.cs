using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class UserInfo : IdentityUser<int>
    {
        public string Name { get; set; }
        public int Pin {  get; set; }
        public string Name { get; set; }
    }
}
