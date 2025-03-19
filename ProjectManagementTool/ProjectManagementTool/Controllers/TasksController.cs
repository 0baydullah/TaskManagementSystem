using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Data;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksService _tasksService;
        private readonly ISubTaskService _subTaskService;
        private readonly IMemberService _memberService;
        private readonly IUserStoryService _userStoryService;

        public TasksController(ITasksService tasksService, ISubTaskService subTaskService, 
            IMemberService memberService, IUserStoryService userStoryService) 
        {
            _tasksService = tasksService;
            _subTaskService = subTaskService;
            _memberService = memberService;
            _userStoryService = userStoryService;
        }

        public IActionResult Index()
        {
            var tasks = _tasksService.GetAllTasks();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var story = _userStoryService.GetUserStory(id);
            ViewBag.ProjectId = story.ProjectId;
            ViewBag.UserStoryId = id;

            var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);
            ViewBag.Members = new SelectList(members, "MemberId", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(int id, Tasks task)
        {
            _tasksService.AddTasks(task);

            if (task == null || id != task.UserStoryId)
            {
                return NotFound();
            }

            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var tasksDetails = new TaskDetailsVM();
            var task = _tasksService.GetTasks(id);
            var subtasks = _subTaskService.GetAllSubTaskByTask(id);

            tasksDetails.Tasks = task;
            tasksDetails.SubTask = subtasks;

            return View(tasksDetails);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var task = _tasksService.GetTasks(id);
            if (task == null)
            {
                return NotFound();
            }

            _tasksService.DeleteTasks(task);
            return Ok();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Tasks task = _tasksService.GetTasks(id);

            //var users = _context.Members.Join(_context.Users, m => m.Email, u => u.Email, (m, u) => new { m, u })
            //    .Where(x => x.m.Email == x.u.Email)
            //    .Select(x => new ResponsibleVM { Id = x.m.MemberId, Name = x.u.Name }).ToList();
            ViewBag.Id = id;
            ViewBag.Members = new SelectList("Id", "Name");

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(int id, Tasks taskVM)
        {
            Tasks task = _tasksService.GetTasks(id);

            if (task == null)
            {
                return NotFound();
            }

            task.Name = taskVM.Name;
            task.Descripton = taskVM.Descripton;
            task.AssignMembersId = taskVM.AssignMembersId;
            task.ReviewerMemberId = taskVM.ReviewerMemberId;
            task.EstimatedTime = taskVM.EstimatedTime;
            task.Tag = taskVM.Tag;
            task.Status = taskVM.Status;
            task.Priority = taskVM.Priority;
            task.UserStoryId = taskVM.UserStoryId;

            _tasksService.UpdateTasks(task);

            return Json(new { success = true });
        }



    }
}
