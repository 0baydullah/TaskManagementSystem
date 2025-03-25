using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class Bug
    {
        [Key]
        public int BugId { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-zA-Z])[\w\s-]+$", ErrorMessage = "Only Numbers or Special Characters are not allowed")]
        public string Name { get; set; }
        public string Descripton { get; set; }

        public int AssignMembersId { get; set; }
        public string? QaRemarks { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public int UserStoryId { get; set; }
    }
}
