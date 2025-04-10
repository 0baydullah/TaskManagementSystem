using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class TodoController : Controller
    {

        private readonly UserManager<UserInfo> _userManager;
        private readonly ITasksService _tasksService;
        private readonly ISubTaskService _subTaskService;
        private readonly IMemberService _memberService;
        private readonly IPriorityService _priorityService;
        private readonly IStatusService _statusService;
        private readonly ILog _log = LogManager.GetLogger(typeof(TodoController));


        public TodoController(
            UserManager<UserInfo> userManager,
            ITasksService tasksService,
            ISubTaskService subTaskService,
            IMemberService memberService,
            IPriorityService priorityService,
            IStatusService statusService)
        {
            _userManager = userManager;
            _tasksService = tasksService;
            _subTaskService = subTaskService;
            _memberService = memberService;
            _priorityService = priorityService;
            _statusService = statusService;
        }


        [HttpGet]
        public async Task<IActionResult> Tasks()
        {
            try
            {
                var todoTasks = new TodoTaskVM();
                var user = await _userManager.GetUserAsync(User);
                var userEmail = user.Email;
                var members = _memberService.GetMemberByEmail(userEmail);
                var tasks = _tasksService.GetAllTasksByMember(members);
                var subTasks = _subTaskService.GetAllSubTaskByMember(members);

                todoTasks.Tasks = tasks;
                todoTasks.SubTasks = subTasks;
                todoTasks.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name);
                todoTasks.StatusList = _statusService.GetAllStatuses().ToDictionary(p => p.StatusId, p => p.Name);

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                return View(todoTasks); 
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
