using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult Exception()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Unauthorize()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Notfound()
        {
            return View();
        }
    }
}
