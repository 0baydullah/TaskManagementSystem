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
        private readonly IBugService _bugService;
        private readonly IMemberService _memberService;
        private readonly IPriorityService _priorityService;
        private readonly IStatusService _statusService;
        private readonly IBugStatusService _bugStatusService;
        private readonly ILog _log = LogManager.GetLogger(typeof(TodoController));


        public TodoController(
            UserManager<UserInfo> userManager,
            ITasksService tasksService,
            ISubTaskService subTaskService,
            IBugService bugService,
            IMemberService memberService,
            IPriorityService priorityService,
            IStatusService statusService,
            IBugStatusService bugStatusService)
        {
            _userManager = userManager;
            _tasksService = tasksService;
            _subTaskService = subTaskService;
            _bugService = bugService;
            _memberService = memberService;
            _priorityService = priorityService;
            _statusService = statusService;
            _bugStatusService = bugStatusService;
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
                todoTasks.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name + "+" + p.ColorHex);
                todoTasks.StatusList = _statusService.GetAllStatuses().ToDictionary(p => p.StatusId, p => p.Name + "+" + p.ColorHex);

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


        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            try
            {
                var toReview = new TodoReviewVM();
                var user = await _userManager.GetUserAsync(User);
                var userEmail = user.Email;
                var members = _memberService.GetMemberByEmail(userEmail);
                var tasks = _tasksService.GetAllTasksByReviewr(members);
                var subTasks = _subTaskService.GetAllSubTaskByReviewr(members);

                toReview.Tasks = tasks;
                toReview.SubTasks = subTasks;
                toReview.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name + "+" + p.ColorHex);
                toReview.StatusList = _statusService.GetAllStatuses().ToDictionary(p => p.StatusId, p => p.Name + "+" + p.ColorHex);

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                return View(toReview); 
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }



        [HttpGet]
        public async Task<IActionResult> Tests()
        {
            try
            {
                var toTests = new TodoTestVM();
                var user = await _userManager.GetUserAsync(User);
                var userEmail = user.Email;
                var members = _memberService.GetMemberByEmail(userEmail);
                var tasks = _tasksService.GetAllTasksByQA(members);
                var subTasks = _subTaskService.GetAllSubTaskByQA(members);

                toTests.Tasks = tasks;
                toTests.SubTasks = subTasks;
                toTests.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name + "+" + p.ColorHex);
                toTests.StatusList = _statusService.GetAllStatuses().ToDictionary(p => p.StatusId, p => p.Name + "+" + p.ColorHex);

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                return View(toTests); 
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Bugs()
        {
            try
            {
                var todoBugs = new TodoBugVM();
                var user = await _userManager.GetUserAsync(User);
                var userEmail = user.Email;
                var members = _memberService.GetMemberByEmail(userEmail);
                var bug = _bugService.GetAllBugByMember(members);

                todoBugs.Bug = bug;
                todoBugs.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name +"+"+ p.ColorHex);
                todoBugs.BugStatusList = _bugStatusService.GetAllStatuses().ToDictionary(p => p.Id, p => p.Name + "+" + p.ColorHex);

                var statuses = _bugStatusService.GetAllStatuses();
                ViewBag.BugStatus = new SelectList(statuses, "StatusId", "Name");

                return View(todoBugs); 
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
