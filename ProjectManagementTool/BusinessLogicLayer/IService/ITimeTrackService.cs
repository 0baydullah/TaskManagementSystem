using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface ITimeTrackService
    {
        public void TimeTrack(int taskId, int subTaskId);
    }
}
