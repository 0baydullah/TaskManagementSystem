using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class ProjectInfo
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Key { get; set; }
        [Required]
        public string? Description { get; set; }

        public List<string>? Files { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string? CompanyName { get; set; }

        [Required]
        public int ProjectOwnerId { get; set; }
    }
}
