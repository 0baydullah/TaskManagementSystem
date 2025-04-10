using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class TodoController : Controller
    {

        private readonly IProjectInfoService _projectInfoService;
        private readonly UserManager<UserInfo> _userManager;
        private readonly ITasksService _tasksService;
        private readonly ILog _log = LogManager.GetLogger(nameof(TodoController));


        public TodoController(IProjectInfoService projectInfoService,
            UserManager<UserInfo> userManager, ITasksService tasksService)
        {

            _projectInfoService = projectInfoService;
            _userManager = userManager;
            _tasksService = tasksService;
        }


        [HttpGet]
        public IActionResult Tasks()
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var tasks= _tasksService.GetAllTasks();
                return View(); 
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }
    }
}
