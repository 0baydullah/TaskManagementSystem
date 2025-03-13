using BusinessLogicLayer.IService;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class SprintController : Controller
    {
        private readonly ISprintService _sprintService;
        private readonly IReleaseService _releaseService;
        private readonly IProjectInfoService _projectInfoService;
        public SprintController(ISprintService sprintService, IReleaseService releaseService, IProjectInfoService projectInfoService)
        {
            _sprintService = sprintService;
            _releaseService = releaseService;
            _projectInfoService = projectInfoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sprints = _sprintService.GetAllSprint();
            var data =  sprints.Select(s => new SprintVM
            {
                SprintId = s.SprintId,
                SprintName = s.SprintName,
                Description = s.Description,
                ProjectKey = _projectInfoService.GetProjectInfo(s.ReleaseId).Key,
                StartDate = s.StartDate,
                EndDate = s.StartDate,
                Points = s.Points,
                Velocity = s.Velocity,
                ReleaseName = _releaseService.GetRelease(s.ReleaseId).ReleaseName,
            }).ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
