using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class SprintVM
    {

        public int SprintId { get; set; }
        [Required]
        public string? SprintName { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? ProjectKey { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int Velocity { get; set; }

        [Required]
        public string? ReleaseName { get; set; }
    }
}
