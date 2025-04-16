using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Threading.Tasks;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class TimeTrackController : Controller
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly ITimeTrackService _timeTrackService;
        private readonly ITasksService _taskService;
        private readonly ISubTaskService _subTaskService;
        private readonly IMemberService _memberService;
        
        private readonly ILog _log = LogManager.GetLogger(typeof(TimeTrackController));

        public TimeTrackController(UserManager<UserInfo> userManager,ITimeTrackService timeTrackService, ITasksService taskService,
            ISubTaskService subTaskService, IMemberService memberService) 
        {
            _userManager = userManager;
            _timeTrackService = timeTrackService;
            _taskService = taskService;
            _subTaskService = subTaskService;
            _memberService = memberService;
        }

        [HttpPost]
        public async Task<IActionResult> Start(int taskId, int subTaskId)
        {
            try
            {
                var result = await _timeTrackService.TimeStoreStart(taskId, subTaskId);
                var isSavedTrackingStatus =  _timeTrackService.UpdateTrackingStatus(taskId,subTaskId, "Started");

                if (result == true)
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
        public async Task<IActionResult> End(int taskId, int subTaskId)
        {
            try
            {
                var isSavedTrackingStatus =  _timeTrackService.UpdateTrackingStatus(taskId, subTaskId, "Stopped");
                var result = await _timeTrackService.TimeStoreEnd(taskId, subTaskId);

                if (result == true)
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
                var timeTracks = _timeTrackService.GetBySubTaskId(subTaskId);

                if (timeTracks != null)
                {
                    var StartTime = timeTracks.Min(t => t.StartTime).ToString("yyyy/MM/dd HH:mm");
                    var EndTime =   timeTracks.Max(t => t.StartTime).ToString("yyyy/MM/dd HH:mm");
                    var TotalTime = timeTracks.Sum(t => t.TotalTime);

                    return Json(new { success = true, StartTime = StartTime, EndTime = EndTime, TotalTime = TotalTime, TimeHistory = timeTracks });
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
                    var StartTime = timeTracks.Min(t => t.StartTime).ToString("yyyy/MM/dd HH:mm");
                    var EndTime = timeTracks.Max(t => t.EndTime).ToString("yyyy/MM/dd HH:mm");
                    var TotalTime = timeTracks.Sum(t => t.TotalTime);

                    return Json(new { success = true, StartTime = StartTime, EndTime = EndTime, TotalTime = TotalTime, TimeHistory = timeTracks });
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
        public IActionResult ExistSubTask(int taskId)
        {
            try
            {
                var subTasks = _subTaskService.GetAllSubTaskByTask(taskId);
                if (subTasks != null)
                {
                   
                    return Json(new { success = true,subTasks = subTasks });
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
        public async Task<IActionResult> IncompletedTimeTrackBySubTask(int subTaskId)
        {
            try
            {
                var incompletedTimeTrack =  await _timeTrackService.IncompletedTimeTrackBySubTask(subTaskId);
                var subTask = _subTaskService.GetSubTask(subTaskId);

                if (incompletedTimeTrack != null) 
                {

                    return Json(new { success = true, status = incompletedTimeTrack.TrackingStatus, timeTrack = incompletedTimeTrack, assignMembersId = subTask.AssignMembersId });
                }
                else
                {
                    return Json(new { success = false, assignMembersId = subTask.AssignMembersId }); 
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
        public async Task<IActionResult> IncompletedTimeTrackByTask(int taskId)
        {
            try
            {
                var incompletedTimeTrack = await _timeTrackService.IncompletedTimeTrackByTask(taskId);
                var task = _taskService.GetTasks(taskId);

                if (incompletedTimeTrack != null)
                {

                    return Json(new { success = true, status = incompletedTimeTrack.TrackingStatus, timeTrack = incompletedTimeTrack, assignMembersId = task.AssignMembersId });
                }
                else
                {
                    return Json(new { success = false, assignMembersId = task.AssignMembersId });
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
		public async Task<IActionResult> DisableButtonTimer()
        {
            return View();
        }
	}
}
