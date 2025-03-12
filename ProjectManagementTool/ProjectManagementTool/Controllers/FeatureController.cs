using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class FeatureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
