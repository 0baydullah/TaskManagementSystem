using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class UserStoryDetailsVM
    {
        public int StoryId { get; set; }
        public UserStory Story { get; set; }
        public List<Tasks> Tasks { get; set; }
        public List<Tasks> Bugs { get; set; }
    }
}
