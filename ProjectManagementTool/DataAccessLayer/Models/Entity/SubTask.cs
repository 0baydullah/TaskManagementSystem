using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class SubTask
    {

        [Key]
        public int SubTaskId { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public int AssignMembersId { get; set; }
        public int ReviewerMemberId { get; set; }
        public double EstimatedTime { get; set; }
        public string Tag { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public int TaskId { get; set; }

        // files will be added later
    }
}
