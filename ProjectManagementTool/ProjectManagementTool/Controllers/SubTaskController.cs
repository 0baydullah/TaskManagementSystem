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
        private readonly PMSDBContext _context;

        public SubTaskController(ISubTaskService subTaskService, PMSDBContext context) 
        {
            _subTaskService = subTaskService;
            _context = context;
        }

        public IActionResult Index()
        {
            var subTask = _subTaskService.GetAllSubTask();
            return View(subTask);
        }

        public IActionResult Create(int id)
        {
            // will be changed with Member service [GetAllMember] Method
            var users = _context.Members.Join(_context.Users, m => m.Email, u => u.Email, (m, u) => new { m, u })
                .Where(x => x.m.Email == x.u.Email)
                .Select(x => new ResponsibleVM{ Id = x.m.MemberId, Name = x.u.Name }).ToList();
            ViewBag.Id = id;
            ViewBag.Members = new SelectList(users, "Id", "Name");

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

            var users = _context.Members.Join(_context.Users, m => m.Email, u => u.Email, (m, u) => new { m, u })
                .Where(x => x.m.Email == x.u.Email)
                .Select(x => new ResponsibleVM { Id = x.m.MemberId, Name = x.u.Name }).ToList();
            ViewBag.Id = id;
            ViewBag.Members = new SelectList(users, "Id", "Name");

            if (subTask == null)
            {
                return NotFound();
            }

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



    }
}
