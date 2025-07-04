﻿using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
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
                var timeTrack = new TimeTrack()
                {
                    TaskId = taskId,
                    SubTaskId = subTaskId,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    TotalTime = 0,
                };

                timeTrack.EndTime = timeTrack.StartTime;
                var result = await _timeTrackRepo.TimeStore(timeTrack);
                
                return result;
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
                        //TodaysTime = DateTime.Now,
                        TotalTime = 0,
                    };

                    timeTrack.EndTime = timeTrack.StartTime;

                    var result = await _timeTrackRepo.TimeStore(timeTrack);
                    return result;
                }
                else
                {

                    existTimeTrack.EndTime = DateTime.Now;
                    existTimeTrack.IsTrackCompleted = true;
                    var spentTime = existTimeTrack.EndTime - existTimeTrack.StartTime;
                    double seconds = spentTime.TotalSeconds;
                    existTimeTrack.TotalTime = existTimeTrack.TotalTime + (long)seconds;
                    var result = await _timeTrackRepo.TimeUpdate(existTimeTrack);
                    
                    return result;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool UpdateTrackingStatus(int taskId, int subTaskId, string status)
        {
            try
            {
                return _timeTrackRepo.UpdateTrackingStatus(taskId, subTaskId, status);
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
                var subTasksTimeTracks = _timeTrackRepo.GetBySubTaskId(subTaskId);

                return subTasksTimeTracks;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<TimeTrack> GetAllByTaskId(int taskId)
        {
            try
            {
                return _timeTrackRepo.GetAllByTaskId(taskId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TimeTrack> IncompletedTimeTrackBySubTask(int subTaskId)
        {
            try
            {
                return await _timeTrackRepo.IncompletedTimeTrackBySubTask(subTaskId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TimeTrack> IncompletedTimeTrackByTask(int taskId)
        {
            try
            {
                return await _timeTrackRepo.IncompletedTimeTrackByTask(taskId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DisableButtonTimer(int disableTime)
        {
            try
            {
                return _timeTrackRepo.DisableButtonTimer(disableTime);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DisableTime GetDisableButtonTimer()
        {
            try
            {
                return _timeTrackRepo.GetDisableButtonTimer();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
