﻿using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITasksService _tasksService;
        private readonly ISubTaskService _subTaskService;
        private readonly IMemberService _memberService;
        private readonly IUserStoryService _userStoryService;
        private readonly ICategoryService _categoryService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly UserManager<UserInfo> _userManager;
        private readonly IProjectInfoService _projectInfoService;

        private readonly ILog _log = LogManager.GetLogger(typeof(TasksController));

        public TasksController(ITasksService tasksService, ISubTaskService subTaskService,
            IMemberService memberService, IUserStoryService userStoryService,
            ICategoryService categoryService, IStatusService statusService,
            IPriorityService prioriyService, UserManager<UserInfo> userManager, IProjectInfoService projectInfoService)
        {
            _tasksService = tasksService;
            _subTaskService = subTaskService;
            _memberService = memberService;
            _userStoryService = userStoryService;
            _categoryService = categoryService;
            _statusService = statusService;
            _priorityService = prioriyService;
            _userManager = userManager;
            _projectInfoService = projectInfoService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var tasks = _tasksService.GetAllTasks();
                throw new Exception("Exception thrown manually for testing");

                return View(tasks);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            try
            {
                var story = _userStoryService.GetUserStory(id);
                var project = _projectInfoService.GetProjectInfo(story.ProjectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;
                ViewBag.UserStoryId = id;

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);
                ViewBag.Members = new SelectList(members, "MemberId", "Name");

                var user = await _userManager.GetUserAsync(User);
                ViewBag.CurrentUserId = user.Id;

                return View();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Create(int id, Tasks task)
        {
            try
            {
                _tasksService.AddTasks(task);

                if (task == null || id != task.UserStoryId)
                {
                    return NotFound();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, int projectId)
        {
            try
            {
                var tasksDetails = new TaskDetailsVM();
                var task = _tasksService.GetTasks(id);
                var subtasks = _subTaskService.GetAllSubTaskByTask(id);
                var user = await _userManager.GetUserAsync(User);
                var memberIds = _memberService.GetAllMember().Where(m => m.ProjectId == projectId && m.Role == "Admin").ToList().Select(i => i.MemberId).ToList();
                var member = _memberService.GetAllMember().FirstOrDefault(m => m.Email == user.Email && m.ProjectId == projectId);
                var project = _projectInfoService.GetProjectInfo(projectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;

                tasksDetails.MemberId = member.MemberId;
                tasksDetails.AdminMemberIds = memberIds;
                tasksDetails.Tasks = task;
                tasksDetails.StoryName = _userStoryService.GetUserStory(task.UserStoryId).StoryName;
                tasksDetails.SubTask = subtasks;
                tasksDetails.StatusList = _statusService.GetAllStatuses().ToDictionary(s => s.StatusId, s => s.Name);
                tasksDetails.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name);
                tasksDetails.MemberList = _memberService.GetAllMember().ToDictionary(m => m.MemberId, m => m.Name);

                return View(tasksDetails);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var task = _tasksService.GetTasks(id);
                if (task == null)
                {
                    return NotFound();
                }

                _tasksService.DeleteTasks(task);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Tasks task = _tasksService.GetTasks(id);

                var story = _userStoryService.GetUserStory(task.UserStoryId);
                ViewBag.ProjectId = story.ProjectId;
                ViewBag.Id = id;

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);
                ViewBag.Members = new SelectList(members, "MemberId", "Name");

                ViewBag.UserStoryId = story.StoryId;

                if (task == null)
                {
                    return NotFound();
                }

                return View(task);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, Tasks taskVM)
        {
            try
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
                task.QAMemberId = taskVM.QAMemberId;
                task.EstimatedTime = taskVM.EstimatedTime;
                task.Tag = taskVM.Tag;
                task.Status = taskVM.Status;
                task.Priority = taskVM.Priority;
                task.UserStoryId = taskVM.UserStoryId;

                _tasksService.UpdateTasks(task);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }
        [HttpPost]
        public IActionResult ChangeStatus(int id, int status)
        {
            try
            {
                Tasks task = _tasksService.GetTasks(id);

                if (task == null)
                {
                    return NotFound();
                }

                task.Status = status;

                _tasksService.UpdateTasks(task);

                return Json(new { success = true });
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
