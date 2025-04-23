using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly ITasksService _tasksService;
        private readonly ISubTaskService _subTaskService;
        private readonly IBugService _bugService;
        private readonly IMemberService _memberService;
        private readonly IPriorityService _priorityService;
        private readonly IStatusService _statusService;
        private readonly IProjectInfoService _projectInfoService;
        private readonly ILog _log = LogManager.GetLogger(typeof(TodoController));

        public DashboardController(UserManager<UserInfo> userManager, ITasksService tasksService, IStatusService statusService, 
            IMemberService memberService, IBugService bugService, IProjectInfoService projectInfoService)
        {
            _userManager = userManager;
            _tasksService = tasksService; 
            _statusService = statusService;
            _memberService = memberService;
            _bugService = bugService;
            _projectInfoService = projectInfoService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new DashboardVM();
                var user = await _userManager.GetUserAsync(User);
                var userEmail = user.Email;
                var members = _memberService.GetMemberByEmail(userEmail);
                var tasks = _tasksService.GetAllTasksByMember(members);
                var bugs = _bugService.GetAllBugByMember(members);
                var status = _statusService.GetAllStatuses();
                var taskResult= status.Join(
                    tasks,
                    s => s.StatusId,
                    t => t.Status, 
                    (s, t) => new
                    {
                        StatusId = s.StatusId,
                        StatusName = s.Name,
                        Color = s.ColorHex
                    }
                ).ToList();

                var taskGroupedData = taskResult
                    .GroupBy(item => new { item.StatusId, item.StatusName, item.Color })
                    .Select(group => new
                    {
                        StatusId = group.Key.StatusId,
                        StatusName = group.Key.StatusName,
                        Color = group.Key.Color,
                        Count = group.Count() 
                    })
                    .ToList();

                var taskModel = new PieChartTaskVM
                {
                    xStatusName = taskGroupedData.Select(g => g.StatusName).ToArray(), 
                    BarColor = taskGroupedData.Select(g => g.Color).ToArray(),         
                    yStatusCount = taskGroupedData.Select(g => g.Count).ToArray()      
                };
                model.PieChartTask = taskModel;

                var bugResult = status.Join(
                    bugs,
                    s => s.StatusId,
                    t => t.BugStatus,
                    (s, t) => new
                    {
                        StatusId = s.StatusId,
                        StatusName = s.Name,
                        Color = s.ColorHex
                    }
                ).ToList();

                var bugGroupedData = bugResult
                    .GroupBy(item => new { item.StatusId, item.StatusName, item.Color })
                    .Select(group => new
                    {
                        StatusId = group.Key.StatusId,
                        StatusName = group.Key.StatusName,
                        Color = group.Key.Color,
                        Count = group.Count()
                    })
                    .ToList();

                var bugModel = new PieChartBugVM
                {
                    xStatusName = bugGroupedData.Select(g => g.StatusName).ToArray(),
                    BarColor = bugGroupedData.Select(g => g.Color).ToArray(),
                    yStatusCount = bugGroupedData.Select(g => g.Count).ToArray()
                };
                model.PieChartBug = bugModel;
                model.Projects = _projectInfoService.GetAllProjectInfo(userEmail);
                model.Bugs = bugs;
                model.Tasks = tasks;

                return View(model);
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
