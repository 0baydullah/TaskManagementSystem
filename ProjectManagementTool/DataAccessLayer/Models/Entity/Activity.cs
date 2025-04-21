
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class Activity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Root { get; set; }
        public string Message { get; set; }
        public int PerformBy {  get; set; }
        public DateTime PerformAt { get; set; }
    }
}
