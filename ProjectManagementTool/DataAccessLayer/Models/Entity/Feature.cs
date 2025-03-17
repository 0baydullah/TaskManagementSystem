using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class Feature
    {
        [Key]
        public int FeatureId {  get; set; }
        [ForeignKey("Release")]
        public int ReleaseId { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public int ProjectId {  get; set; }
        public double EstimatedPoint { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
    }
}
