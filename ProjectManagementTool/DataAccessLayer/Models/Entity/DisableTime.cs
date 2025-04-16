using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class DisableTime
    {
        [Key]
        public int Id { get; set; }
        public int Time { get; set; } 
    }
}
