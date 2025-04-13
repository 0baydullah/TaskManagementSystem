using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class PieChartTaskVM
    {
        
        public string[] xStatusName { get; set; }
        public int[] yStatusCount { get; set; }
        public string[] BarColor { get; set; }

    }
}
