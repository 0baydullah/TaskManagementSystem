using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class TimeTrackController : Controller
    {
        private readonly ITimeTrackService _timeTrackService;
        private readonly ISubTaskService _subTaskService;
        private readonly ILog _log = LogManager.GetLogger(typeof(TimeTrackController));

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
                var isSavedTrackingStatus = _timeTrackService.UpdateTrackingStatus(subTaskId, "Started");
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
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
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
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult GetTimeBySubTaskId(int subTaskId) 
        {
            try
            {
                var timeTrack = _timeTrackService.GetBySubTaskId(subTaskId);

                if (timeTrack != null)
                {
                    var StartTime = timeTrack.StartTime.ToString("MM/dd/yyyy HH:mm");
                    var EndTime = timeTrack.EndTime.ToString("MM/dd/yyyy HH:mm");
                    var TotalTime = timeTrack.TotalTime;

                    return Json(new { success = true, StartTime = StartTime, EndTime = EndTime, TotalTime = TotalTime });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult GetAllTimeByTaskId(int taskId)
        {
            try
            {
                var timeTracks = _timeTrackService.GetAllByTaskId(taskId);
                if (timeTracks.Count != 0)
                {
                    var StartTime = timeTracks.Min(t => t.StartTime).ToString("MM/dd/yyyy HH:mm");
                    var EndTime = timeTracks.Max(t => t.EndTime).ToString("MM/dd/yyyy HH:mm");
                    var TotalTime = timeTracks.Sum(t => t.TotalTime);
                    return Json(new { success = true, StartTime = StartTime, EndTime = EndTime, TotalTime = TotalTime });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }
    }
}
