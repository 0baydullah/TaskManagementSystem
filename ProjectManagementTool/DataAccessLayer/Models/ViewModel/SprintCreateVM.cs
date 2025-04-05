using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class SprintCreateVM
    {
        public int SprintId { get; set; }

        [Required(ErrorMessage = "Sprint Name is required!")]
        //[RegularExpression(@"^(?=.*[a-zA-Z])[\w\s-]+$", ErrorMessage = "Only Numbers or Special Characters are not allowed")]
        [DisplayName(displayName: "Sprint Name")]
        public string? SprintName { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [DisplayName(displayName: "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Start Date is required!")]
        [DisplayName(displayName: "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Time Duration is required!")]
        [DisplayName(displayName: "Duration")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Points is required!")]
        [DisplayName(displayName: "Points")]
        public int Points { get; set; }

        [Required(ErrorMessage = "Velocity is required!")]
        [DisplayName(displayName: "Velocity")]
        public int Velocity { get; set; }

        [Required(ErrorMessage = "Release is required!")]
        public int ReleaseId { get; set; }
    }
}
