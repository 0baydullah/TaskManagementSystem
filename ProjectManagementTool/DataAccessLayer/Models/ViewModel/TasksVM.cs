﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class TasksVM
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-zA-Z])[\w\s-]+$", ErrorMessage = "Only Numbers or Special Characters are not allowed")]
        public string Name { get; set; }
        public string Descripton { get; set; }

        public int AssignMembersId { get; set; }
        public int ReviewerMemberId { get; set; }
        public int QAMemberId { get; set; }
        public double EstimatedTime { get; set; }
        [Required]
        public string Tag { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public int UserStoryId { get; set; }
        public string TrackingStatus {  get; set; }
        public int SubTaskCount { get; set; }
    }
}
