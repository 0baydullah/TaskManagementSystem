using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class EditProjectInfoVM
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public string Key { get; set; }
        public string Description { get; set; }

        public List<string>? ExistingFiles { get; set; }
        public List<IFormFile>? Files { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CompanyName { get; set; }

        public int ProjectOwnerId { get; set; }
    }
}
