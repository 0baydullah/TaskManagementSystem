using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Enums
{
    public enum Status
    {
        Planned = 1,
        [Description("In Progress")] InProgress,
        Close,
        Done,
    }
}
