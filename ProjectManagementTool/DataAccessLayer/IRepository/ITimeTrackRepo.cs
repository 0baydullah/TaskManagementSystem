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
        public Task<TimeTrack> GetByTaskIdSubTaskId(int taskId, int subTaskId);
        public Task<bool> TimeStore(TimeTrack timeTrack);
        public Task<bool> TimeUpdate(TimeTrack timeTrack);
    }
}
