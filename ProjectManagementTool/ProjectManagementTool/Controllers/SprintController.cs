using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class SprintController : Controller
    {
        private readonly ISprintService _sprintService;
        private readonly IReleaseService _releaseService;
        private readonly IProjectInfoService _projectInfoService;
        private readonly ILog _log = LogManager.GetLogger(typeof(SprintController));
        
        public SprintController(ISprintService sprintService, IReleaseService releaseService, IProjectInfoService projectInfoService)
        {
            _sprintService = sprintService;
            _releaseService = releaseService;
            _projectInfoService = projectInfoService;
        }

        [HttpGet]
        public IActionResult Index(int projectId)
        {
            try
            {
                var sprints = _sprintService.GetAllSprint(projectId);
                var project = _projectInfoService.GetProjectInfo(projectId);
                var data = sprints.Select(s => new SprintVM
                {
                    SprintId = s.SprintId,
                    SprintName = s.SprintName,
                    Description = s.Description,
                    StartDate = s.StartDate,
                    EndDate = s.StartDate.AddDays(s.Duration - 1),
                    Points = s.Points,
                    Velocity = s.Velocity,
                    ReleaseName = _releaseService.GetRelease(s.ReleaseId).ReleaseName,
                }).ToList();
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;

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
                var releases = _releaseService.GetAllReleases().Where(r => r.ProjectId == projectId).ToList();
                var project = _projectInfoService.GetProjectInfo(projectId);
                ViewBag.Releases = new SelectList(releases, "ReleaseId", "ReleaseName");
                ViewBag.ProjectId = project.ProjectId;
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
        public IActionResult Create(SprintCreateVM sprint)
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
                
                var response = _sprintService.AddSprint(sprint);
                if( response == true)
                {
                    isSuccess = true;
                    message = "Sprint added successfully!";
                    _log.Info(message);
                }
                else
                {
                    isSuccess = false;
                    message = "Sprint alredy exist!";
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
        public IActionResult CreateWithRelease(int releaseId)
        {
            try
            {
                var release = _releaseService.GetRelease(releaseId);
                var project = _projectInfoService.GetProjectInfo(release.ProjectId);
                ViewBag.ReleaseId = releaseId;
                ViewBag.ProjectId = project.ProjectId;
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
        public IActionResult CreateWithRelease(SprintCreateVM sprint)
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

                var response = _sprintService.AddSprint(sprint);
                if (response == true)
                {
                    isSuccess = true;
                    message = "Sprint added successfully!";
                    _log.Info(message);
                }
                else
                {
                    isSuccess = false;
                    message = "Sprint alredy exist!";
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
        public IActionResult Edit(int id, int projectId)
        {
            try
            {
                var sprint = _sprintService.GetSprint(id);
                var releases = _releaseService.GetAllReleases().Where(r => r.ProjectId == projectId);
                ViewBag.Releases = new SelectList(releases, "ReleaseId", "ReleaseName", sprint.ReleaseId);
                ViewBag.ProjectId = projectId;
                
                return View(sprint);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SprintCreateVM sprint, int id)
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

            try
            {
               
                var response = await _sprintService.UpdateSprint(id, sprint);
                if(response == true)
                {
                    isSuccess = true;
                    message = "Sprint updated successfully!";
                    _log.Info(message);
                }
                else
                {
                    isSuccess = false;
                    message = "Sprint already exist!";
                    _log.Error(message);
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
            bool isSuccess = true;
            string message = "Sprint deleted successfully!";
            try
            {
                var sprint = _sprintService.GetSprint(id);
                _sprintService.DeleteSprint(sprint);
                _log.Info(message);

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
        public IActionResult Details(int id, int projectId, string projectKey)
        {
            try
            {
                var sprint = _sprintService.GetSprintDetails(id);
                ViewBag.ProjectId = projectId;
                ViewBag.ProjectKey = projectKey;
                
                return View(sprint);
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
