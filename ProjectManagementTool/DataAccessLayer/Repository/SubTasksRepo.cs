using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using log4net;
using DataAccessLayer.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class SubTasksRepo : ISubTasksRepo
    {
        private readonly PMSDBContext _context;
        private readonly ILog _log = LogManager.GetLogger(typeof(SubTasksRepo));

        public SubTasksRepo(PMSDBContext context)
        {
            _context = context;
        }

        public void AddSubTask(SubTask subTask)
        {
            try
            {
                _context.SubTask.Add(subTask);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSubTask(SubTask subTask)
        {
            try
            {
                _context.SubTask.Remove(subTask);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAllAssociation(int id)
        {
            try
            {
                var subTask = _context.SubTask.Where(x => x.TaskId == id).ToList();
                _context.SubTask.RemoveRange(subTask);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<SubTask> GetAllSubTask()
        {
            try
            {
                var subTask = _context.SubTask.ToList();
                return subTask;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<SubTaskVM> GetAllSubTaskByTask(int id)
        {
            try
            {
                var subTasks = _context.SubTask.Where(x=>x.TaskId == id ).ToList();
                var subTaskWithTimeTrack = subTasks.GroupJoin(_context.TimeTracks.Where(ttrack => ttrack.IsTrackCompleted == false), s => s.SubTaskId, t => t.SubTaskId, (s, t) => new { s,t }).SelectMany(
                    x => x.t.DefaultIfEmpty(), (x, t) => new SubTaskVM
                    {
                        SubTaskId = x.s.SubTaskId,
                        Name = x.s.Name,
                        Descripton = x.s.Descripton,
                        AssignMembersId = x.s.AssignMembersId,
                        ReviewerMemberId = x.s.ReviewerMemberId,
                        EstimatedTime = x.s.EstimatedTime,
                        Tag = x.s.Tag,
                        TaskId = x.s.TaskId,
                        Status = x.s.Status,
                        Priority = x.s.Priority,
                        StartTime = t != null ? t.StartTime.ToString() : " ",
                        EndTime = t != null ? t.StartTime.ToString() : " ",
                        TotalTime = t != null ? t.TotalTime : 0.0,
                    }).ToList();

                return subTaskWithTimeTrack;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public SubTask GetSubTask(int id)
        {
            try
            {
                var subTask = _context.SubTask.FirstOrDefault(x => x.SubTaskId == id);
                return subTask;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateSubTask(SubTask subTask)
        {
            try
            {
                _context.SubTask.Update(subTask);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
