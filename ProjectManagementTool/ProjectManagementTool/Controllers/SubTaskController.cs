using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Data;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class SubTaskController : Controller
    {
        private readonly ISubTaskService _subTaskService;
        private readonly IMemberService _memberService;
        private readonly IUserStoryService _userStoryService;
        private readonly ITasksService _tasksService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly IProjectInfoService _projectInfoService;

        private readonly ILog _log = LogManager.GetLogger(typeof(SubTaskController));

        public SubTaskController(ISubTaskService subTaskService, IMemberService memberService, IUserStoryService userStoryService, 
            ITasksService tasksService, IStatusService statusService, IPriorityService prioriyService, IProjectInfoService projectInfoService)
        {
            _subTaskService = subTaskService;
            _memberService = memberService;
            _userStoryService = userStoryService;
            _tasksService = tasksService;
            _statusService = statusService;
            _priorityService = prioriyService;
            _projectInfoService = projectInfoService;
        }


        [HttpGet]
        public IActionResult Err()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index()
        {
            try
            {
                var subTask = _subTaskService.GetAllSubTask();
                return View(subTask);
            }
            catch (Exception ex)
            {
                _log.Error("Something went wrong : " + ex.Message);
                var errorModel = new ErrVM
                {
                    Msg = ex.Message,
                    ActionMetod = "Index",
                    CustomMsg = "this is a custom message",
                    StackTrace = ex.StackTrace
                };

                return View("Err", errorModel);
            }
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            try
            {
                var task = _tasksService.GetTasks(id);
                var story = _userStoryService.GetUserStory(task.UserStoryId);
                var project = _projectInfoService.GetProjectInfo(story.ProjectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;

                ViewBag.Id = id;

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);
                ViewBag.Members = new SelectList(members, "MemberId", "Name");

                return View();
            }
            catch (Exception ex)
            {
                _log.Error("Something went wrong : " + ex.Message);
                var errorModel = new ErrVM
                {
                    Msg = ex.Message,
                    ActionMetod = "Create Get",
                    CustomMsg = "this is a custom message",
                    StackTrace = ex.StackTrace
                };

                return View("Err", errorModel);
            }
        }

        [HttpPost]
        public IActionResult Create(int id, SubTask subTask)
        {
            try
            {
                _subTaskService.AddSubTask(subTask);

                if (subTask == null || id != subTask.TaskId)
                {
                    return NotFound();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _log.Error("Something went wrong : " + ex.Message);
                var errorModel = new ErrVM
                {
                    Msg = ex.Message,
                    ActionMetod = "Create Post",
                    CustomMsg = "this is a custom message",
                    StackTrace = ex.StackTrace
                };

                return View("Err", errorModel);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var subTask = _subTaskService.GetSubTask(id);

                if (subTask == null)
                {
                    return NotFound();
                }
                var task = _tasksService.GetTasks(subTask.TaskId);
                var story = _userStoryService.GetUserStory(task.UserStoryId);
                var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);

                ViewBag.Id = id;
                ViewBag.Members = new SelectList(members, "MemberId", "Name");

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                return View(subTask);
            }
            catch (Exception ex)
            {
                _log.Error("Something went wrong : " + ex.Message);
                var errorModel = new ErrVM
                {
                    Msg = ex.Message,
                    ActionMetod = "Create Get",
                    CustomMsg = "this is a custom message",
                    StackTrace = ex.StackTrace
                };

                return View("Err", errorModel);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, SubTask subTaskVM)
        {
            try
            {
                var subTask = _subTaskService.GetSubTask(id);

                if (subTask == null)
                {
                    return NotFound();
                }

                subTask.Name = subTaskVM.Name;
                subTask.Descripton = subTaskVM.Descripton;
                subTask.AssignMembersId = subTaskVM.AssignMembersId;
                subTask.ReviewerMemberId = subTaskVM.ReviewerMemberId;
                subTask.EstimatedTime = subTaskVM.EstimatedTime;
                subTask.Tag = subTaskVM.Tag;
                subTask.Status = subTaskVM.Status;
                subTask.Priority = subTaskVM.Priority;
                subTask.TaskId = subTaskVM.TaskId;

                _subTaskService.UpdateSubTask(subTask);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _log.Error("Something went wrong : " + ex.Message);
                var errorModel = new ErrVM
                {
                    Msg = ex.Message,
                    ActionMetod = "Edit Post",
                    CustomMsg = "this is a custom message",
                    StackTrace = ex.StackTrace
                };

                return View("Err", errorModel);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var subTask = _subTaskService.GetSubTask(id);
                if (subTask == null)
                {
                    return NotFound();
                }

                _subTaskService.DeleteSubTask(subTask);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.Error("Something went wrong : " + ex.Message);
                var errorModel = new ErrVM
                {
                    Msg = ex.Message,
                    ActionMetod = "Delete",
                    CustomMsg = "this is a custom message",
                    StackTrace = ex.StackTrace
                };

                return View("Err", errorModel);
            }
        }
    }
}
