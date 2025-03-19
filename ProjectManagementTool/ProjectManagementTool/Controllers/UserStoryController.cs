using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjectManagementTool.Controllers
{
    public class UserStoryController : Controller
    {
        private readonly IUserStoryService _userStoryService;
        private readonly ITasksService _tasksService;

        public UserStoryController(IUserStoryService userStoryService, ITasksService tasksService)
        {
            _userStoryService = userStoryService;
            _tasksService = tasksService;
        }

        [HttpGet]
        public IActionResult Index(int projectId)
        {
            var userStories = _userStoryService.GetAllUserStory().Where( s => s.ProjectId == projectId);
            ViewBag.ProjectId = projectId;

            return View(userStories);
        }

        [HttpGet]
        public IActionResult Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
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
            var tasks = _tasksService.GetAllTasks(id);
            var bugs = tasks; // Bug will be added later after implementation

            storyDetails.Story = story;
            storyDetails.Tasks = tasks;
            storyDetails.Bugs = bugs;

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
