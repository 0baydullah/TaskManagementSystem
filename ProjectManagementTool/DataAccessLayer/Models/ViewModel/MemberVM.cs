using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class MemberVM
    {
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int ProjectId { get; set; }
    }
}
