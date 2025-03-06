using BusinessLogicLayer.IService;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class UserStoryController : Controller
    {

        private readonly IUserStoryService _userStoryService;
        public UserStoryController(IUserStoryService userStoryService)
        {
            _userStoryService = userStoryService;
        }
        public IActionResult Index()
        {
            var userStories = _userStoryService.GetAllUserStory();
            return View(userStories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserStory userStory)
        {
            _userStoryService.AddUserStory(userStory);
            return RedirectToAction("Index");
        }
    }
}
