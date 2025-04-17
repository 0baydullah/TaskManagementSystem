using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.StaticClass
{
    public static class TodoHelper
    {
        public static readonly List<int> DevsStatus = new List<int> { 4, 5, 8, 11, 12, 16, 17 };
        public static readonly List<int> ReviewrsStatus = new List<int> { 9, 10, 13 };
        public static readonly List<int> QAsStatus = new List<int> { 14, 15 };
    }
}
