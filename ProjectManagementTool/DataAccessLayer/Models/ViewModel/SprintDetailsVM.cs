using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class SprintDetailsVM
    {
        public int SprintId { get; set; }
        public string SprintName { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        public DateTime EndDate { get; set; }
        public int Points { get; set; }
        public int Velocity { get; set; }
        public List<UserStory>? UserStroy { get; set; }

    }
}
