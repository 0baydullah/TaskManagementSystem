using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface ITimeTrackService
    {
        public Task<bool> TimeStoreStart(int taskId, int subTaskId);
        public Task<bool> TimeStoreEnd(int taskId, int subTaskId);
    }
}
