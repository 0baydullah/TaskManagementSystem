using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class TaskDetailsVM
    {
        public Tasks Tasks { get; set; }
        public List<SubTask> SubTask { get; set; }
    }
}
