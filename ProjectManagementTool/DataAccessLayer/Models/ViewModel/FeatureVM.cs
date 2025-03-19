using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class FeatureVM
    {
        public int ReleaseId { get; set; }
        public int MemberId { get; set; }
        public int ProjectId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public double EstimatedPoint { get; set; }
        public string Tag { get; set; }
    }
}
