using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class BugStatus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        [Required]
        public string ColorHex { get; set; }
    }
}
