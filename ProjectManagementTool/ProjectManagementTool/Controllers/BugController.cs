﻿using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class BugController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IUserStoryService _userStoryService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly IBugService _bugService;
        private readonly IProjectInfoService _projectInfoService;
        private readonly ITasksService _tasksService;
        private readonly UserManager<UserInfo> _userManager;

        private readonly ILog _log = LogManager.GetLogger(typeof(BugController));

        public BugController(IMemberService memberService, IUserStoryService userStoryService, IStatusService statusService,
            IPriorityService priorityService, IBugService bugService, IProjectInfoService projectInfoService,
            ITasksService tasksService, UserManager<UserInfo> userManager)
        {
            _memberService = memberService;
            _userStoryService = userStoryService;
            _statusService = statusService;
            _priorityService = priorityService;
            _bugService = bugService;
            _projectInfoService = projectInfoService;
            _tasksService = tasksService;
            _userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult Create(int id)
        {
            try
            {
                var story = _userStoryService.GetUserStory(id);
                var project = _projectInfoService.GetProjectInfo(story.ProjectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;
                ViewBag.StoryId = id;
                ViewBag.UserStoryId = id;
                
                var loggedInUsersEmail = _userManager.GetUserAsync(HttpContext.User).Result.Email;
                var loggedInMember = _memberService.GetAllMember().FirstOrDefault(x => x.Email == loggedInUsersEmail && x.ProjectId == project.ProjectId).MemberId;
                ViewBag.LoggedInMember = loggedInMember;

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);
                ViewBag.Members = new SelectList(members, "MemberId", "Name");

                var tasks = _tasksService.GetAllTasks(id);
                ViewBag.Tasks = new SelectList(tasks, "TaskId", "Name");

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
        public IActionResult Create(int id, BugVM bugVM)
        {
            try
            {
               
                
                if (bugVM == null )
                {
                    return Json(new { success = false, messgage = "Story Not Found" });
                }
                
                _bugService.AddBug(id,bugVM);

                

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
        public IActionResult Delete(int id)
        {
            try
            {
                var bug = _bugService.GetBug(id);
                if (bug == null)
                {
                    return RedirectToAction("Notfound","Error");
                }

                _bugService.DeleteBug(bug);
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
                var bug = _bugService.GetBug(id);
                var story = _userStoryService.GetUserStory(bug.UserStoryId);
                var project = _projectInfoService.GetProjectInfo(story.ProjectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;
                ViewBag.StoryId = story.StoryId;
                ViewBag.Id = id;

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);
                ViewBag.Members = new SelectList(members, "MemberId", "Name");
                
                var tasks = _tasksService.GetAllTasks(story.StoryId);
                ViewBag.Tasks = new SelectList(tasks, "TaskId", "Name");

                ViewBag.UserStoryId = bug.UserStoryId;

                if (bug == null)
                {
                    return RedirectToAction("Notfound","Error");
                }

                return View(bug);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, BugVM bug)
        {
            try
            {
                var existingBug = _bugService.GetBug(id);

                if (existingBug == null)
                {
                    return RedirectToAction("Notfound", "Error");
                }

                _bugService.UpdateBug(id,bug);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        public IActionResult Details(int id, int projectId)
        {
            try
            {
                var bugDetails = new BugDetailsVM();
                var bug = _bugService.GetBug(id);
                var project = _projectInfoService.GetProjectInfo(projectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;
                ViewBag.StoryId = bug.UserStoryId;
                bugDetails.Bug = bug;
                bugDetails.StoryName = _userStoryService.GetUserStory(bug.UserStoryId).StoryName;
                bugDetails.StatusList = _statusService.GetAllStatuses().ToDictionary(s => s.StatusId, s => s.Name);
                bugDetails.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name);
                bugDetails.MemberList = _memberService.GetAllMember().ToDictionary(m => m.MemberId, m => m.Name);

                return View(bugDetails);
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
                var bug = _bugService.GetBug(id);

                if (bug == null)
                {
                    return NotFound();
                }

                bug.BugStatus = status;

                _bugService.UpdateBug(bug);

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
        public IActionResult Reopen(int id)
        {
            try
            {
                var bug = _bugService.GetBug(id);

                if (bug == null)
                {
                    return NotFound();
                }

                bug.BugStatus = 36;
                bug.BugReopen = ++bug.BugReopen;

                _bugService.UpdateBug(bug);

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
