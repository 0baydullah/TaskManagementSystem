using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class ProjectDetailsVM
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int Sprint { get; set; }
        public int Release { get; set; }
        public int Member { get; set; }
        public int UserStory { get; set; }
        public int Task { get; set; }
        public int Bug { get; set; }
        public int Feature { get; set; }
    }
}
