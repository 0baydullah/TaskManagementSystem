using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class MemberWithRoleVM
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int ProjectId { get; set; }
        //
        public int TotalTask { get; set; }

        public int TotalBug { get; set; }
    }
}
