using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.StaticClass
{
    public static class ActionNameHelper
    {
        public const string IndexActionName = "Index";
        public const string DetailsActionName = "Details";
        public const string EditActionName = "Edit";
        public const string CreateActionName = "Create";
        public const string GetAllUserActionName = "GetAllUser";
    }
    public static class ControllerNameHelper
    {
        public const string UserStoryControllerName = "UserStory";
        public const string ProjectControllerName = "Project";
        public const string TasksControllerName = "Tasks";
        public const string SubTasksControllerName = "SubTask";
        public const string ReleaseControllerName = "Release";
        public const string SprintControllerName = "Sprint";
        public const string MemberControllerName = "Member";
    }
}
