using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class ReleaseController : Controller
    {
        private readonly IReleaseService _releaseService;
        private readonly IProjectInfoService _projectInfoService;
        public ReleaseController( IReleaseService releaseService, IProjectInfoService projectInfoService)
        {
            _releaseService = releaseService;
            _projectInfoService = projectInfoService;
        }
        public IActionResult Index()
        {
            var releases = _releaseService.GetAllReleases();
            var data = releases.Select(r => new ReleaseVM
            {
                ReleaseId = r.ReleaseId,
                ProjectKey = _projectInfoService.GetProjectInfo(r.ProjectId).Key,
                ReleaseName = r.ReleaseName,
                Description = r.Description,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Sprints = "0"
            }).ToList();

            return View(data);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var projects = _projectInfoService.GetAllProjectInfo();
            ViewBag.Projects = new SelectList(projects, "ProjectId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Release release)
        {
            bool isSuccess = false;
            string message = "Invalid Data Submitted!";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select( m => m.ErrorMessage);

                foreach (var error in errors)
                {
                    message += error + " ";
                    Console.WriteLine(error);
                }
            }
            else 
            {
                _releaseService.AddRelease(release);
                isSuccess = true;
                message = "Release added successfully!";
            }

            return Json(new { isSuccess, message });
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

    }
}
