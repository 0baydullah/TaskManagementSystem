using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProjectManagementTool.Controllers
{
    public class TimeTrackController : Controller
    {
        private readonly ITimeTrackService _timeTrackService;
        public TimeTrackController(ITimeTrackService timeTrackService) 
        { 
            _timeTrackService = timeTrackService;
        }

        [HttpPost]
        public IActionResult Start(int taskId, int subTaskId)
        {
            try
            {
                var result = _timeTrackService.TimeStoreStart(taskId, subTaskId);
                return Ok(new { message = "Tracking started" });
                //if (result != null)
                //{
                //    return Ok(new { success = true });
                //}
                //else
                //{
                //    return Ok(new { success = false, errors = "dont start" });
                //}
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult End(int taskId, int subTaskId)
        {
            try
            {
                var result = _timeTrackService.TimeStoreEnd(taskId, subTaskId);
                return Ok(new { message = "Tracking started" });
                //if (result != null)
                //{
                //    return Ok(new { success = true });
                //}
                //else
                //{
                //    return Ok(new { success = false, errors = "dont stop" });
                //}
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
