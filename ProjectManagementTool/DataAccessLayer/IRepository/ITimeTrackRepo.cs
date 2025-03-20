using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ITimeTrackRepo
    {
        public void TimeTrack(int taskId, int subTaskId);
    }
}
