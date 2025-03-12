using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }

        public int AssignMembersId { get; set; }
        public int ReviewerMemberId { get; set; }
        public double EstimatedTime { get; set; }
        public string Tag { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public int UserStoryId { get; set; }

        // files will be added later

    }
}
