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
        private readonly PMSDBContext _context;

        public TasksController(ITasksService tasksService, PMSDBContext context) 
        {
            _tasksService = tasksService;
            _context = context;
        }

        public IActionResult Index()
        {
            var tasks = _tasksService.GetAllTasks();
            return View(tasks);
        }

        public IActionResult Create(int id)
        {
            // will be changed to layered
            var users = _context.Members.Join(_context.Users, m => m.Email, u => u.Email, (m, u) => new { m, u })
                .Where(x => x.m.Email == x.u.Email)
                .Select(x => new ResponsibleVM{ Id = x.m.MemberId, Name = x.u.Name }).ToList();
            ViewBag.Id = id;
            ViewBag.Members = new SelectList(users, "Id", "Name");

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
    }
}
