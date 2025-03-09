using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.Models.ViewModel
{
    public class UserStoryVM
    {

        [Required]
        [MaxLength(255)]
        public string StoryName { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public float Points { get; set; }

        public double EstimateTime { get; set; }

        public Status Status { get; set; }

        public Priority Priority { get; set; }

        public int SprintId { get; set; } = -1;

        // public IEnumerable<IFormFile> Files { get; set; }  

        // Navigation Properties
        //public Sprint Sprint { get; set; }
    }
}
