using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
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
                ViewBag.ProjectId = projectId;
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

                return View(data);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Opps Exception Occurr: " + ex.Message;
                _log.Error(ViewBag.Error);

                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Create(int projectId)
        {
            try
            {
                var releases = _releaseService.GetAllReleases().Where(r => r.ProjectId == projectId).ToList();
                ViewBag.Releases = new SelectList(releases, "ReleaseId", "ReleaseName");
                ViewBag.ProjectId = projectId;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Opps Exception Occurr: " + ex.Message;
                _log.Error(ViewBag.Error);

                return View();
            }
            
        }

        [HttpPost]
        public IActionResult Create(Sprint sprint)
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
            catch (Exception)
            {

                throw;
            }
            
        }

        
        [HttpGet]
        public IActionResult Edit(int id, int projectId)
        {
            var sprint = _sprintService.GetSprint(id);
            var releases = _releaseService.GetAllReleases().Where( r => r.ProjectId == projectId);
            ViewBag.Releases = new SelectList(releases, "ReleaseId", "ReleaseName",sprint.ReleaseId);
            ViewBag.ProjectId = projectId;
            return View(sprint);
        }

        [HttpPost]
        public IActionResult Edit(Sprint sprint, int id)
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

                var data = _sprintService.GetSprint(id);
                if (data == null)
                {
                    return NotFound("Data is not exist");
                }

                data.SprintName = sprint.SprintName;
                data.Description = sprint.Description;
                data.StartDate = sprint.StartDate;
                data.Duration = sprint.Duration;
                data.Points = sprint.Points;
                data.Velocity = sprint.Velocity;
                data.ReleaseId = sprint.ReleaseId;


                _sprintService.UpdateSprint(data);
                isSuccess = true;
                message = "Sprint updated successfully!";

            }

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var sprint = _sprintService.GetSprint(id);

            if (sprint == null)
            {
                return NotFound("Sprint not found!");
            }

            _sprintService.DeleteSprint(sprint);
            return Json(new { success = "true", message = "Sprint deleted successfully!" });

        }
    }
}
