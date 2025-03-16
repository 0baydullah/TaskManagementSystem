using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Index(int projectId)
        {
            var sprints = _sprintService.GetAllSprint(projectId);
            //var data =  sprints.Select(s => new SprintVM
            //{
            //    SprintId = s.SprintId,
            //    SprintName = s.SprintName,
            //    Description = s.Description,
            //    //ProjectKey = _projectInfoService.GetProjectInfo(s.ReleaseId).Key,
            //    StartDate = s.StartDate,
            //    EndDate = s.StartDate,
            //    Points = s.Points,
            //    Velocity = s.Velocity,
            //    ReleaseName = _releaseService.GetRelease(s.ReleaseId).ReleaseName,
            //}).ToList();

            return View(sprints);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var releases = _releaseService.GetAllReleases();
            ViewBag.Releases = new SelectList(releases, "ReleaseId", "ReleaseName");
            return View();
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
            }
            else
            {
                _sprintService.AddSprint(sprint);
                isSuccess = true;
                message = "Sprint added successfully!";
            }

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sprint = _sprintService.GetSprint(id);
            var releases = _releaseService.GetAllReleases();
            ViewBag.Releases = new SelectList(releases, "ReleaseId", "ReleaseName",sprint.ReleaseId);
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
