using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
