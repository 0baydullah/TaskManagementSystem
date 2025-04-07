using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class UserStoryController : Controller
    {
        private readonly IUserStoryService _userStoryService;
        private readonly ITasksService _tasksService;
        private readonly IMemberService _memberService;
        private readonly ICategoryService _categoryService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly ISprintService _sprintService;
        private readonly ITimeTrackService _timeTrackService;
        private readonly IBugService _bugService;

        private readonly ILog _log = LogManager.GetLogger(typeof(UserStoryController));

        public UserStoryController(IUserStoryService userStoryService, ITasksService tasksService,
            IMemberService memberService, ICategoryService categoryService, IStatusService statusService,
            IPriorityService prioriyService, ISprintService sprintService, ITimeTrackService timeTrackService,
            IBugService bugService)
        {
            _userStoryService = userStoryService;
            _tasksService = tasksService;
            _memberService = memberService;
            _categoryService = categoryService;
            _statusService = statusService;
            _priorityService = prioriyService;
            _sprintService = sprintService;
            _timeTrackService = timeTrackService;
            _bugService = bugService;
        }

        [HttpGet]
        public IActionResult Index(int projectId)
        {
            try
            {
                var storyList = new UserStoryListVM();
                var userStories = _userStoryService.GetAllUserStory().Where(s => s.ProjectId == projectId).ToList();

                ViewBag.ProjectId = projectId;

                storyList.UserStories = userStories;
                storyList.MemberList = _memberService.GetAllMember().ToDictionary(m => m.MemberId, m => m.Name);
                storyList.StatusList = _statusService.GetAllStatuses().ToDictionary(s => s.StatusId, s => s.Name);
                storyList.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name);
                storyList.CategoryList = _categoryService.GetAllCategory().ToDictionary(c => c.CategoryId, c => c.Name);

                return View(storyList);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create(int projectId)
        {
            try
            {
                ViewBag.ProjectId = projectId;
                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                var categories = _categoryService.GetAllCategory();
                ViewBag.Category = new SelectList(categories, "CategoryId", "Name");

                var sprints = _sprintService.GetAllSprint(projectId);
                ViewBag.Sprints = new SelectList(sprints, "SprintId", "SprintName");

                return View();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(UserStory userStory)
        {
            try
            {
                if (userStory == null)
                {
                    return NotFound();
                }

                _userStoryService.AddUserStory(userStory);

                return Ok(new { success = true, redirectUrl = @Url.Action("Index", "UserStory", new { projectId = userStory.ProjectId }) });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var storyDetails = new UserStoryDetailsVM();
                var story = _userStoryService.GetUserStory(id);
                ViewBag.ProjectId = story.ProjectId;
                var tasks = _tasksService.GetAllTasks(id);
                var bugs = _bugService.GetAllBugOfStory(id);

                storyDetails.Story = story;
                storyDetails.Tasks = tasks;
                storyDetails.Bugs = bugs;
                storyDetails.MemberList = _memberService.GetAllMember().ToDictionary(m => m.MemberId, m => m.Name);
                storyDetails.StatusList = _statusService.GetAllStatuses().ToDictionary(s => s.StatusId, s => s.Name);
                storyDetails.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name);
                storyDetails.CategoryList = _categoryService.GetAllCategory().ToDictionary(c => c.CategoryId, c => c.Name);
                storyDetails.SprintList = _sprintService.GetAllSprint(story.ProjectId).ToDictionary(c => c.SprintId, c => c.SprintName);

                return View(storyDetails);
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
                UserStory userStory = _userStoryService.GetUserStory(id);

                if (userStory == null)
                {
                    return NotFound();
                }

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                var categories = _categoryService.GetAllCategory();
                ViewBag.Category = new SelectList(categories, "CategoryId", "Name");

                var sprints = _sprintService.GetAllSprint(userStory.ProjectId);
                ViewBag.Sprints = new SelectList(sprints, "SprintId", "SprintName");

                return View(userStory);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, UserStoryVM storyVM)
        {
            try
            {
                UserStory userStory = _userStoryService.GetUserStory(id);

                if (userStory == null)
                {
                    return NotFound();
                }

                userStory.StoryName = storyVM.StoryName;
                userStory.Description = storyVM.Description;
                userStory.Category = storyVM.Category;
                userStory.Points = storyVM.Points;
                userStory.EstimateTime = storyVM.EstimateTime;
                userStory.Status = storyVM.Status;
                userStory.Priority = storyVM.Priority;
                userStory.SprintId = storyVM.SprintId;
                userStory.ProjectId = storyVM.ProjectId;

                _userStoryService.UpdateUserStory(userStory);

                return Ok(new { success = true, redirectUrl = @Url.Action("Index", "UserStory", new { projectId = userStory.ProjectId }) });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userStory = _userStoryService.GetUserStory(id);

                if (userStory == null)
                {
                    return NotFound();
                }

                _userStoryService.DeleteUserStory(userStory);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return StatusCode(500, ex.Message);
            }
        }
    }
}
