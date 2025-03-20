using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Models.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public string ColorHex { get; set; }
    }
}
