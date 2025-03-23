using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class TimeTrackService : ITimeTrackService
    {
        private readonly ITimeTrackRepo _timeTrackRepo;
        public TimeTrackService(ITimeTrackRepo timeTrackRepo) 
        {
            _timeTrackRepo = timeTrackRepo;
        }

        public async Task<bool> TimeStoreStart(int taskId, int subTaskId)
        {
            try
            {
                var existTimeTrack = _timeTrackRepo.GetByTaskIdSubTaskId(taskId, subTaskId);

                if (existTimeTrack == null)
                {
                    var timeTrack = new TimeTrack()
                    {
                        TaskId = taskId,
                        SubTaskId = subTaskId,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        TodaysTime = DateTime.Now,
                        TotalTime = 0,
                    };

                    var result = await _timeTrackRepo.TimeStore(timeTrack);
                    return result;
                }
                else
                {
                    existTimeTrack.TodaysTime = DateTime.Now;
                    var result = await _timeTrackRepo.TimeUpdate(existTimeTrack);
                    
                    return result;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> TimeStoreEnd(int taskId, int subTaskId)
        {
            try
            {
                var existTimeTrack = _timeTrackRepo.GetByTaskIdSubTaskId(taskId, subTaskId);

                if (existTimeTrack == null)
                {
                    var timeTrack = new TimeTrack()
                    {
                        TaskId = taskId,
                        SubTaskId = subTaskId,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        TodaysTime = DateTime.Now,
                        TotalTime = 0,
                    };

                    var result = await _timeTrackRepo.TimeStore(timeTrack);
                    return result;
                }
                else
                {

                    existTimeTrack.EndTime = DateTime.Now;
                    var spentTime = existTimeTrack.EndTime - existTimeTrack.TodaysTime;
                    existTimeTrack.TotalTime = existTimeTrack.TotalTime + spentTime.Seconds;
                    var result = await _timeTrackRepo.TimeUpdate(existTimeTrack);
                    
                    return result;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool UpdateTrackingStatus(int subTaskId, string status)
        {
            return _timeTrackRepo.UpdateTrackingStatus(subTaskId, status);
        }
    }
}
