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
        public bool UpdateTrackingStatus(int taskId, int subTaskId, string status);
        public List<TimeTrack> GetBySubTaskId(int subTaskId);
        public List<TimeTrack> GetAllByTaskId(int taskId);
        public Task<TimeTrack> IncompletedTimeTrackBySubTask(int subTaskId);
        public Task<TimeTrack> IncompletedTimeTrackByTask(int taskId);
        public bool DisableButtonTimer();
        public int GetDisableButtonTimer();
    }
}
