using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IFilterRepo
    {
        public void InProgress(string status, int projectId);
    }
}
