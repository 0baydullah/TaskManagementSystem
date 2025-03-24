using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class ReleaseCreateVM
    {
        [Required(ErrorMessage = "Release Name is required!")]
        [RegularExpression(@"^(?=.*[a-zA-Z])[\w\s-]+$", ErrorMessage = "Only Numbers or Special Characters are not allowed")]
        [DisplayName(displayName: "Release Name")]
        public string ReleaseName { get; set; }
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start Date is required!")]
        [DisplayName(displayName: "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required!")]
        [DisplayName(displayName: "End Date")]
        public DateTime EndDate { get; set; }
        public string? Sprints { get; set; }

    }
}
