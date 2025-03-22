using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
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
        private readonly ISprintService _sprintService;
        public ReleaseController( IReleaseService releaseService, IProjectInfoService projectInfoService,
            ISprintService sprintService)
        {
            _releaseService = releaseService;
            _projectInfoService = projectInfoService;
            _sprintService = sprintService;
        }
        
        public IActionResult Index(int projectId)
        {
            var releases = _releaseService.GetAllReleases().Where(r => r.ProjectId == projectId).ToList();
            var data = releases.Select(r => new ReleaseVM
            {
                ReleaseId = r.ReleaseId,
                ProjectKey = _projectInfoService.GetProjectInfo(r.ProjectId).Key,
                ReleaseName = r.ReleaseName,
                Description = r.Description,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Sprints = _sprintService.GetAllSprint(projectId).Where(s => s.ReleaseId == r.ReleaseId).Count().ToString()
            }).ToList();
            ViewBag.ProjectId = projectId;

            return View(data);
        }
        
        [HttpGet]
        public IActionResult Create(int projectId)
        {
            //var projects = _projectInfoService.GetAllProjectInfo();
            //ViewBag.Projects = new SelectList(projects, "ProjectId", "Name");
            ViewBag.ProjectId = projectId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Release release)
        {
            try
            {
                bool isSuccess = false;
                string message = "Invalid Data Submitted!";

                if (ModelState.IsValid == false)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage);

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

                return Json(new { success = $"{isSuccess}", message = $"{message}" });

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //var projects = _projectInfoService.GetAllProjectInfo();
            //ViewBag.Projects = new SelectList(projects, "ProjectId", "Name", release.ProjectId);
            var release = _releaseService.GetRelease(id);
            return View(release);
        }

        [HttpPost]
        public IActionResult Edit(Release release, int id)
        {
            bool isSuccess = false;
            string message = "Invalid Data Submitted!";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage);
                foreach (var error in errors)
                {
                    message += error + " ";
                    Console.WriteLine(error);
                }
            }
            else
            {

                var data = _releaseService.GetRelease(id);
                if (data == null)
                {
                    return NotFound("Data is not exist");
                }

                data.ReleaseName = release.ReleaseName;
                data.Description = release.Description;
                data.StartDate = release.StartDate;
                data.EndDate = release.EndDate;
                data.ProjectId = release.ProjectId;

                _releaseService.UpdateRelease(data);
                isSuccess = true;
                message = "Release updated successfully!";
                
            }

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var release = _releaseService.GetRelease(id);

            if (release == null)
            {
                return NotFound("Release not found!");
            }

            _releaseService.DeleteRelease(release);
            return Json(new { success = "true", message = "Release deleted successfully!" });

        }


    }
}
