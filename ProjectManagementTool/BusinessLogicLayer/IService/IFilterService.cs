using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IFilterService
    {
        public void InProgress(string status, int projectId);
    }
}
