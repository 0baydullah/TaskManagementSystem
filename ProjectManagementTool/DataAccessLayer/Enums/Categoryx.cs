using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Enums
{
    public enum Categoryx
    {
        Development = 1,
        [Description("Testing & Fixing")] TestingFixing,
        [Description("Meeting & Discussion")] MeetingDiscussion,
        Learning,
        [Description("Project Management")] ProjectManagement,
        [Description("Code Review")] Review,
        RND,
        None
    }
}
