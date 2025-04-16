using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class PieChartBugVM
    {
        public string[] xStatusName { get; set; }
        public int[] yStatusCount { get; set; }
        public string[] BarColor { get; set; }
    }
}
