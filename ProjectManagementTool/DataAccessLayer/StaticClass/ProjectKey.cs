using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.StaticClass
{
    public static class ProjectKey
    {
        static int ProjectId = 0;
        static string KeyName = string.Empty;

        public static void SetProjectId(int id)
        {
            ProjectId = id;
        }


        public static void SetProjectKey(string key)
        {
            KeyName = key;
        }

        public static int GetProjectId()
        {
            return ProjectId;
        }

        public static string GetProjectKey()
        {
            return KeyName;
        }
    }
}
