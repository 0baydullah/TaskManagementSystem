using DataAccessLayer.Models.Entity;
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
        public bool UpdateTrackingStatus(int subTaskId, string status);
        public TimeTrack GetBySubTaskId(int subTaskId);
        public TimeTrack GetByTaskId(int taskId);
    }
}
