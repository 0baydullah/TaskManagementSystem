using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class ReleaseVM
    {
        public int ReleaseId { get; set; }
        public string ReleaseName { get; set; }
        public string ProjectKey { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Sprints { get; set; }
    }
}
