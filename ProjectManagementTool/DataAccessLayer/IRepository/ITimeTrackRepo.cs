using DataAccessLayer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ITimeTrackRepo
    {
        public TimeTrack GetByTaskIdSubTaskId(int taskId, int subTaskId);
        public Task<bool> TimeStore(TimeTrack timeTrack);
        public Task<bool> TimeUpdate(TimeTrack timeTrack);
        public bool UpdateTrackingStatus(int taskId, int subTaskId, string status);
        public List<TimeTrack> GetBySubTaskId(int subTaskId);
        public List<TimeTrack> GetAllByTaskId(int taskId); 
        public Task<TimeTrack> IncompletedTimeTrackBySubTask(int subTaskId);
        public Task<TimeTrack> IncompletedTimeTrackByTask(int taskId); 
    }
}
