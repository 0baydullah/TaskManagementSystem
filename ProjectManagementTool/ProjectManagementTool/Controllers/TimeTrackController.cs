using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProjectManagementTool.Controllers
{
    public class TimeTrackController : Controller
    {
        private readonly ITimeTrackService _timeTrackService;
        private readonly ISubTaskService _subTaskService;

        public TimeTrackController(ITimeTrackService timeTrackService, ISubTaskService subTaskService) 
        { 
            _timeTrackService = timeTrackService;
            _subTaskService = subTaskService;
        }

        [HttpPost]
        public IActionResult Start(int taskId, int subTaskId)
        {
            try
            {
                var isSavedTrackingStatus = _timeTrackService.UpdateTrackingStatus(subTaskId, "Stopped");
                var result = _timeTrackService.TimeStoreStart(taskId, subTaskId);

                if (result.Result == true)
                {
                    return Ok(new { success = true, message = "Tracking started", status = "Started" });
                }
                else
                {
                    return Ok(new { success = false, errors = "don't start tracking" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult End(int taskId, int subTaskId)
        {
            try
            {
                var isSavedTrackingStatus = _timeTrackService.UpdateTrackingStatus(subTaskId, "Stopped");
                var result = _timeTrackService.TimeStoreEnd(taskId, subTaskId);

                if (result.Result == true)
                {
                    return Ok(new { success = true, message = "Tracking stopped",status = "Stopped" });
                }
                else
                {
                    return Ok(new { success = false, errors = "don't stop tracking" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetTimeDetails(int subTaskId)
        {
            var subTask = _subTaskService.GetAllSubTaskByTask(subTaskId);
            if (subTask == null)
                return NotFound();

            var response = subTask.Select(x => new
            {
                Description = x.Descripton,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                TotalTime = x.TotalTime
            }).FirstOrDefault();

            return Json(response);
        }
    }
}
