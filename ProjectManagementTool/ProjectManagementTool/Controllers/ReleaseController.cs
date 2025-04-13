using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class ReleaseController : Controller
    {
        private readonly IReleaseService _releaseService;
        private readonly IProjectInfoService _projectInfoService;
        private readonly ISprintService _sprintService;
        private readonly ILog _log = LogManager.GetLogger(typeof(SprintController));
        public ReleaseController( IReleaseService releaseService, IProjectInfoService projectInfoService,
            ISprintService sprintService)
        {
            _releaseService = releaseService;
            _projectInfoService = projectInfoService;
            _sprintService = sprintService;
        }
        
        public IActionResult Index(int projectId)
        {
            try
            {
                var releases = _releaseService.GetAllReleases().Where(r => r.ProjectId == projectId).ToList();
                var projectKey = _projectInfoService.GetProjectInfo(projectId).Key;
                var data = releases.Select(r => new ReleaseVM
                {
                    ReleaseId = r.ReleaseId,
                    ProjectKey = projectKey,
                    ReleaseName = r.ReleaseName,
                    Description = r.Description,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Sprints = _sprintService.GetAllSprint(projectId).Where(s => s.ReleaseId == r.ReleaseId).Count().ToString() ?? "No Sprint",
                }).ToList();
                ViewBag.ProjectId = projectId;
                ViewBag.ProjectKey = projectKey;
                
                return View(data);

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
            
        }
        
        [HttpGet]
        public IActionResult Create(int projectId)
        {
            try
            {
                var project = _projectInfoService.GetProjectInfo(projectId);
                ViewBag.ProjectId = projectId;
                ViewBag.ProjectKey = project.Key;

                return View();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
           
        }

        [HttpPost]
        public IActionResult Create(ReleaseCreateVM release)
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

                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }
            
            try
            {
                
                var response = _releaseService.AddRelease(release);
                if (response == false)
                {
                    isSuccess = false;
                    message = "Release already exist!";
                    _log.Info(message);
                }
                else
                {
                    isSuccess = true;
                    message = "Release created successfully!";
                    _log.Info(message);
                }

                return Json(new { success = $"{isSuccess}", message = $"{message}" });
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
                var release = _releaseService.GetRelease(id);
                var project = _projectInfoService.GetProjectInfo(release.ProjectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;

                return View(release);
            }
            catch (Exception ex)
            {

                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }  
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Release release, int id)
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

                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }
            try
            { 
                var response = await _releaseService.UpdateRelease( id, release);
                if(response == true)
                {
                    isSuccess = true;
                    message = "Release updated successfully!";
                    _log.Info(message);
                }
                else
                {
                    isSuccess = false;
                    message = "Release already exist!";
                    _log.Info(message);

                }
               

                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
            
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool success = true;
            var message = "Release deleted successfully!";
            try
            {   
                var release = _releaseService.GetRelease(id);
                var sprint = _sprintService.GetAllSprint(release.ProjectId).Where( s => s.ReleaseId == id).FirstOrDefault();
                if(sprint == null)
                {
                    _releaseService.DeleteRelease(release);
                    _log.Info(message);
                }
                else
                {
                    success = false ;
                    message = "Release cannot be deleted!";
                    _log.Info(message);
                }

                return Json(new { success, message });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var release = _releaseService.GetReleaseDetails(id);
                var project = _projectInfoService.GetProjectInfo(release.ProjectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;

                return View(release);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }
    }
}
