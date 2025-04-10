using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class TodoController : Controller
    {

        private readonly UserManager<UserInfo> _userManager;
        private readonly ITasksService _tasksService;
        private readonly IMemberService _memberService;
        private readonly IPriorityService _priorityService;
        private readonly ILog _log = LogManager.GetLogger(typeof(TodoController));


        public TodoController(
            UserManager<UserInfo> userManager, ITasksService tasksService,
            IMemberService memberService, IPriorityService priorityService)
        {
            _memberService = memberService;
            _userManager = userManager;
            _tasksService = tasksService;
            _priorityService = priorityService;
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
                var tasks = _tasksService.GetAllTasks().Join(members,t=>t.AssignMembersId, m => m.MemberId, (t,m)=> new Tasks{
                    TaskId = t.TaskId,
                    Name = t.Name,
                    Descripton = t.Descripton,
                    AssignMembersId = t.AssignMembersId,
                    ReviewerMemberId = t.ReviewerMemberId,
                    EstimatedTime = t.EstimatedTime,
                    Tag = t.Tag,
                    Status = t.Status,
                    Priority = t.Priority,
                    UserStoryId = t.UserStoryId
                }).ToList();
                todoTasks.Tasks = tasks;


                todoTasks.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name);

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
