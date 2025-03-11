using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class MemberVM
    {
        public int MemberId { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
