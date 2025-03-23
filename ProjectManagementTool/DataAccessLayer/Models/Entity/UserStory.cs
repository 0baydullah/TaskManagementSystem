using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Entity
{
    public class UserStory
    {
        [Key]
        public int StoryId { get; set; }

        [Required]
        [MaxLength(255)]
        [RegularExpression(@"^(?=.*[a-zA-Z])[\w\s-]+$", ErrorMessage = "Only Numbers or Special Characters are not allowed")]
        public string StoryName { get; set; }
        [Required(ErrorMessage ="Hey fool ! you shoud fill up description!")]
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
