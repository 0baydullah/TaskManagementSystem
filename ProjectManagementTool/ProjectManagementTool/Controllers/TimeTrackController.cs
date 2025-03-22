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

                if (result.Result == true)
                {
                    return Ok(new { success = true, message = "Tracking started" });
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
                var result = _timeTrackService.TimeStoreEnd(taskId, subTaskId);

                if (result.Result == true)
                {
                    return Ok(new { success = true, message = "Tracking stopped" });
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
    }
}
