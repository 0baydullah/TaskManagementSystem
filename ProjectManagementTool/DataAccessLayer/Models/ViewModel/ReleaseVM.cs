﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class ReleaseVM
    {
        public int ReleaseId { get; set; }

        [Required(ErrorMessage = "Release Name is required!")]
        [RegularExpression(@"^(?=.*[a-zA-Z])[\w\s-]+$", ErrorMessage = "Only Numbers or Special Characters are not allowed")]
        [DisplayName(displayName: "Release Name")]
        public string ReleaseName { get; set; }

        [Required(ErrorMessage = "Project Key is required!")]
        [DisplayName(displayName: "Project Key")]
        public string? ProjectKey { get; set; }

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
