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
    public class SubTaskController : Controller
    {
        private readonly ISubTaskService _subTaskService;
        private readonly IMemberService _memberService;
        private readonly PMSDBContext _context;
        private readonly IUserStoryService _userStoryService;
        private readonly ITasksService _tasksService;

        public SubTaskController(ISubTaskService subTaskService, PMSDBContext context, IMemberService memberService
            , IUserStoryService userStoryService, ITasksService tasksService) 
        {
            _subTaskService = subTaskService;
            _memberService = memberService;
            _context = context;
            _userStoryService = userStoryService;
            _tasksService = tasksService;
        }

        public IActionResult Index()
        {
            var subTask = _subTaskService.GetAllSubTask();
            return View(subTask);
        }

        public IActionResult Create(int id)
        {
            
            var task = _tasksService.GetTasks(id);
            var story = _userStoryService.GetUserStory(task.UserStoryId);
            var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);

            ViewBag.Id = id;
            ViewBag.Members = new SelectList(members, "MemberId", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Create(int id, SubTask subTask)
        {
            _subTaskService.AddSubTask(subTask);

            if (subTask == null || id != subTask.TaskId)
            {
                return NotFound();
            }

            return Json(new { success = true });
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var subTask = _subTaskService.GetSubTask(id);

            if (subTask == null)
            {
                return NotFound();
            }
            var task = _tasksService.GetTasks( subTask.TaskId);
            var story = _userStoryService.GetUserStory(task.UserStoryId);
            var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);

            ViewBag.Id = id;
            ViewBag.Members = new SelectList(members, "MemberId", "Name");

            return View(subTask);
        }

        [HttpPost]
        public IActionResult Edit(int id, SubTask subTaskVM)
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


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var subTask = _subTaskService.GetSubTask(id);
            if (subTask == null)
            {
                return NotFound();
            }

            _subTaskService.DeleteSubTask(subTask);
            return Ok();
        }

       



    }
}
