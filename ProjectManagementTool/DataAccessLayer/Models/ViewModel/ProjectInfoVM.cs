using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class ProjectInfoVM
    {
        public int ProjectId { get; set; } 
        [Required(ErrorMessage ="Project Name is required!")]
        [DisplayName(displayName: "Project Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Project Key is required!")]
        [DisplayName(displayName: "Project Key")]
        [RegularExpression(@"^[A-Z]{2,5}$", ErrorMessage = "Key must be 2 to 5 uppercase letters!")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Project Description is required!")]
        [DisplayName(displayName: "Description")]
        public string Description { get; set; }

        public List<IFormFile>? Files { get; set; }

        [Required(ErrorMessage = "Project Start Date is required!")]
        [DisplayName(displayName: "Start Date")]
        //[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Project End Date is required!")]
        [DisplayName(displayName: "End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Company Name is required!")]
        [DisplayName(displayName: "Company Name")]
        public string CompanyName { get; set; }

        public int ProjectOwnerId { get; set; }

        public Dictionary<int, string>? MemberList { get; set; }
    }
}
