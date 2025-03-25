using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class BugController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
