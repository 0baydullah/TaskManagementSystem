using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
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
            var existTimeTrack =  _context.TimeTracks.FirstOrDefault(t => t.TaskId == taskId && t.SubTaskId == subTaskId && t.StartTime == t.EndTime);

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

        public bool UpdateTrackingStatus(int subTaskId, string status)
        {
            try
            {
                var timeTrack = _context.TimeTracks.FirstOrDefault(s => s.SubTaskId == subTaskId && s.IsTrackCompleted == false);
               
                if (timeTrack == null)
                {
                    return false;
                }

                timeTrack.TrackingStatus = status;
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TimeTrack> GetBySubTaskId(int subTaskId)
        {
            try
            {
                var subTask = _context.TimeTracks.Where(s => s.SubTaskId == subTaskId).ToList();
                
                return subTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<TimeTrack> GetAllByTaskId(int taskId) 
        {
            try
            {
                var task = _context.TimeTracks.Where(s => s.TaskId == taskId).ToList();
                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
