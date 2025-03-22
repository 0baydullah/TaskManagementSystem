using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
       
        [Required]
        public string? Email { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        // Navigation properties

        public ICollection<ProjectInfo> ProjectInfo { get; set; }

    }
}
