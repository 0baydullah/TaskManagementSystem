using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class ErrVM
    {
        public string Msg { get; set; }
        public string ActionMetod { get; set; }
        public string? CustomMsg { get; set; }
        public string? StackTrace { get; set; }
    }
}
