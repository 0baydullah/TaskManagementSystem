using DataAccessLayer.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProjectManagementTool.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<UserInfo> _signInManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<UserInfo> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

      
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User) == true)
            {
                return RedirectToAction("Index", "Project");
            }
            else
            {
                return View();
            }
        }


        public IActionResult Welcome()
        {
            return View();
        }
       
    }
}
