using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class ProjectInfo
    {
        [Key]
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }
        public string Description { get; set; }

        public List<string>? Files { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CompanyName { get; set; }

        public int ProjectOwnerId { get; set; }
    }
}
