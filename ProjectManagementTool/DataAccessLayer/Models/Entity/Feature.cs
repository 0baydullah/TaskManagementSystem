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
        [Required]
        public int ReleaseId { get; set; }
        [ForeignKey("Member")]
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int ProjectId {  get; set; }
        [Required]
        public double EstimatedPoint { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Tag { get; set; }
    }
}
