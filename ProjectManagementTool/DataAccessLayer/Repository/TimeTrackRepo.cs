using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class TimeTrackRepo : ITimeTrackRepo
    {
        private readonly PMSDBContext _context;

        public TimeTrackRepo(PMSDBContext context)
        {
            _context = context;
        }

        public TimeTrack GetByTaskIdSubTaskId(int taskId, int subTaskId)
        {
            var existTimeTrack =  _context.TimeTracks.FirstOrDefault(t => t.TaskId == taskId && t.SubTaskId == subTaskId);

            return existTimeTrack;
        }
        public async Task<bool> TimeStore(TimeTrack timeTrack)
        {
            try
            {
                if (timeTrack == null)
                {
                    return false;
                }

                _context.TimeTracks.Add(timeTrack);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> TimeUpdate(TimeTrack timeTrack)
        {
            try
            {
                if (timeTrack == null)
                {
                    return false;
                }

                _context.TimeTracks.Update(timeTrack);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
