using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Entity
{
    public class UserStory
    {
        [Key]
        public int StoryId { get; set; }

        [Required]
        [MaxLength(255)]
        public string StoryName { get; set; }

        public string Description { get; set; }

        public int Category { get; set; }

        public float Points { get; set; }

        public double EstimateTime { get; set; } = 0; // estimated time will be move later

        public int Status { get; set; }

        public int Priority { get; set; }

        public int SprintId { get; set; } = 0;

        public int ProjectId { get; set; }

        // public IEnumerable<IFormFile> Files { get; set; }  

        // Navigation Properties
        //public Sprint Sprint { get; set; }
    }
}
