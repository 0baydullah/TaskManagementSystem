using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class RoleVM
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        public int RoleId { get; set; }
    }
}
