using BusinessLogicLayer.IService;
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
        private readonly ILog _log = LogManager.GetLogger(typeof(TodoController));

        public DashboardController(UserManager<UserInfo> userManager, ITasksService tasksService, IStatusService statusService)
        {
            _userManager = userManager;
            _tasksService = tasksService; 
            _statusService = statusService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userEmail = user.Email;
                var members = _memberService.GetMemberByEmail(userEmail);
                var tasks = _tasksService.GetAllTasksByMember(members);
                var status = _statusService.GetAllStatuses();

                var taskModel = new PieChartTaskVM();
                taskModel.xStatusName = status.Select(status => status.Name).ToArray();
                taskModel.BarColor = status.Select(status => status.ColorHex).ToArray();
                
                return View();
            }
            catch (Exception)
            {

                throw;
            }

           
        }
    }
}
