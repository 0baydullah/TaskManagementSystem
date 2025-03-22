using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class AllUserVM
    {
        public int Id { get; set; }
        public int Pin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<string>? Projects { get; set; }

        public int? ProjectId { get; set; }
    }
}
