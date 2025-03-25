using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                var data = releases.Select(r => new ReleaseVM
                {
                    ReleaseId = r.ReleaseId,
                    ProjectKey = _projectInfoService.GetProjectInfo(r.ProjectId).Key,
                    ReleaseName = r.ReleaseName,
                    Description = r.Description,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Sprints = _sprintService.GetAllSprint(projectId).Where(s => s.ReleaseId == r.ReleaseId).Count().ToString() ?? "No Sprint",
                }).ToList();
                ViewBag.ProjectId = projectId;
                
                return View(data);

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Opps! Exception Occurred: " + ex.Message;
                _log.Error(ViewBag.Error);
                
                return View();
            }
            
        }
        
        [HttpGet]
        public IActionResult Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
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
                isSuccess = false;
                message = "Opps! Exception Occurred: " + ex.Message;
                _log.Error(message);
                
                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var release = _releaseService.GetRelease(id);
                
                return View(release);
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Opps! Exception Occurred: " + ex.Message;
                _log.Error(ViewBag.Error);

                return View();
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
                isSuccess = false;
                message = "Opps! Exception Occurred: " + ex.Message;
                _log.Error(message);

                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }
            
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool isSuccess = true;
            var message = "Project deleted successfully!";
            try
            {
                var release = _releaseService.GetRelease(id);
                _releaseService.DeleteRelease(release);
                _log.Info(message);

                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = "Opps! Exception Occurred: " + ex.Message;
                _log.Error(message);
                
                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }
        }
    }
}
