using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class FeatureWithMemberReleaseVM
    {
        public int FeatureId { get; set; }
        public int ReleaseId { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double EstimatedPoint { get; set; }
        public string Tag { get; set; }
        public string MemberName { get; set; }
        public string ReleaseName { get; set; }
    }
}
