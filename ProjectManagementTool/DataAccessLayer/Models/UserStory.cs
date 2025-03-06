using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UserStory
    {
        [Key]
        public int StoryId { get; set; }

        [Required]
        [MaxLength(255)]
        public string StoryName { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public float Points { get; set; }

        public double EstimateTime { get; set; }

        public int Status { get; set; }

        public int Priority { get; set; }

        public int SprintId { get; set; }

        // Navigation Properties
        //public Sprint Sprint { get; set; }
        //public Category Category { get; set; }
    }
}
