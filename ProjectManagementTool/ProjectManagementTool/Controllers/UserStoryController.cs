using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace ProjectManagementTool.Controllers
{
    public class UserStoryController : Controller
    {
        private readonly IUserStoryService _userStoryService;
        private readonly ITasksService _tasksService;
        private readonly IMemberService _memberService;
        private readonly ICategoryService _categoryService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly ISprintService _sprintService;

        public UserStoryController(IUserStoryService userStoryService, ITasksService tasksService,
            IMemberService memberService, ICategoryService categoryService, IStatusService statusService,
            IPriorityService prioriyService, ISprintService sprintService)
        {
            _userStoryService = userStoryService;
            _tasksService = tasksService;
            _memberService = memberService;
            _categoryService = categoryService;
            _statusService = statusService;
            _priorityService = prioriyService;
            _sprintService = sprintService;
        }

        [HttpGet]
        public IActionResult Index(int projectId)
        {
            var storyList = new UserStoryListVM();
            var userStories = _userStoryService.GetAllUserStory().Where( s => s.ProjectId == projectId).ToList();

            ViewBag.ProjectId = projectId;

            storyList.UserStories = userStories;
            storyList.MemberList = _memberService.GetAllMember().ToDictionary(m => m.MemberId, m => m.Name);
            storyList.StatusList = _statusService.GetAllStatuses().ToDictionary(s => s.StatusId, s => s.Name);
            storyList.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name);
            storyList.CategoryList = _categoryService.GetAllCategory().ToDictionary(c => c.CategoryId, c => c.Name);

            return View(storyList);
        }

        [HttpGet]
        public IActionResult Create(int projectId)
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

        [HttpPost]
        public IActionResult Create(UserStory userStory)
        {
            _userStoryService.AddUserStory(userStory);

            if (userStory == null)
            {
                return NotFound();
            }

            return Ok(new { success = true, redirectUrl = @Url.Action("Index", "UserStory", new { projectId = userStory.ProjectId }) });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var storyDetails = new UserStoryDetailsVM();
            var story = _userStoryService.GetUserStory(id);
            ViewBag.ProjectId = story.ProjectId;
            var tasks = _tasksService.GetAllTasks(id);
            var bugs = tasks; // Bug will be added later after implementation

            storyDetails.Story = story;
            storyDetails.Tasks = tasks;
            storyDetails.Bugs = bugs;
            storyDetails.MemberList = _memberService.GetAllMember().ToDictionary(m=> m.MemberId, m=> m.Name);
            storyDetails.StatusList = _statusService.GetAllStatuses().ToDictionary(s => s.StatusId, s => s.Name);
            storyDetails.PriorityList = _priorityService.GetAllPriority().ToDictionary(p => p.PriorityId, p => p.Name);
            storyDetails.CategoryList = _categoryService.GetAllCategory().ToDictionary(c => c.CategoryId, c => c.Name);
            storyDetails.SprintList = _sprintService.GetAllSprint(story.ProjectId).ToDictionary(c => c.SprintId, c => c.SprintName);

            return View(storyDetails);
        }


        [HttpGet]
        public IActionResult Edit(int id)
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
            ViewBag.Sprints = new SelectList(sprints, "SprintId", "Name");

            return View(userStory);
        }
        
        [HttpPost]
        public IActionResult Edit(int id, UserStoryVM storyVM)
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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var userStory = _userStoryService.GetUserStory(id);

            if (userStory == null)
            {
                return NotFound();
            }

            _userStoryService.DeleteUserStory(userStory);

            return Json(new { success = true });
        }
    }
}
