using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class TimeTrackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
