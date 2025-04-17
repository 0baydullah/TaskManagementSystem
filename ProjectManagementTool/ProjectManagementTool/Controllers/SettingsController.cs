using BusinessLogicLayer.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        public ITimeTrackService _timeTrackService;

        public SettingsController(ITimeTrackService timeTrackService)
        {
            _timeTrackService = timeTrackService;
        }   

        [HttpGet]
        public ActionResult Index()
        {
            var disableTime = _timeTrackService.GetDisableButtonTimer();
            return View(disableTime);
        }

    }
}
