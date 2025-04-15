using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class DashboardVM
    {
        public PieChartTaskVM? PieChartTask { get; set; }
        public PieChartBugVM? PieChartBug { get; set; }
        public List<ProjectInfo> Projects { get; set; }

    }
}
