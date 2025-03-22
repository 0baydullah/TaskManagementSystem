using BusinessLogicLayer.IService;
using DataAccessLayer.Data;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectInfoService _projectInfoService;
        private readonly UserManager<UserInfo> _userManager;
        private readonly IReleaseService _releaseService;
        private readonly ISprintService _sprintService;

        public ProjectController( IProjectInfoService projectInfoService,
            UserManager<UserInfo> userManager, IReleaseService releaseService, ISprintService sprintService)
        {
            
            _projectInfoService = projectInfoService;
            _userManager = userManager;
            _releaseService = releaseService;
            _sprintService = sprintService;
        }

        public IActionResult Index()
        {
            try
            {
                var projects = _projectInfoService.GetAllProjectInfo();
                return View(projects);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProjectInfoVM model)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted! ";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                    message += error + " ";
                }
                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }

            try
            {
                   
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    isSuccess = false;
                    message = "User not found!";
                    return Json(new { isSuccess, message });
                }

                var response = _projectInfoService.AddProjectInfo(model, user);
                if (response == false)
                {
                    isSuccess = false;
                    message = "Project not created!";
                }
                else
                {
                    isSuccess = true;
                    message = "Project created successfully!";
                }
                return Json(new { success = $"{isSuccess}", message = $"{message}" });

            }
            catch (Exception)
            {

                return View(model);
            } 
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var project = _projectInfoService.GetProjectInfo(id);
                if (project == null)
                {
                    return NotFound("Project not found! ");
                }

                else
                {
                    var model = new EditProjectInfoVM
                    {
                        ProjectId = project.ProjectId,
                        Name = project.Name,
                        Key = project.Key,
                        Description = project.Description,
                        StartDate = project.StartDate,
                        EndDate = project.EndDate,
                        CompanyName = project.CompanyName,
                        ProjectOwnerId = project.ProjectOwnerId,
                    };

                    return View(model);
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPost]
        public IActionResult Edit(EditProjectInfoVM model, int id)
        {
            bool isSuccess = false;
            var message = "Invalid Data!";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                    message += error + " ";
                }
                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }

            try
            {
                
                var project = _projectInfoService.GetProjectInfo(id);
                if (project == null)
                {
                    isSuccess = false;
                    message = "Project not found!";

                    return Json(new { success = $"{isSuccess}", message = $"{message}" });
                }

                _projectInfoService.UpdateProjectInfo(model);
                isSuccess = true;
                message = "Data updated successfully!";
                

                return Json(new { success = $"{isSuccess}", message = $"{message}" });

            }
            catch (Exception)
            {

                return View(model);
            }           
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var isSuccess = false;
                var message = "Project not Found!";
                var response = _projectInfoService.GetProjectInfo(id);

                if (response != null)
                {
                    isSuccess = true;
                    message = "Project deleted successfully!";
                    _projectInfoService.DeleteProjectInfo(response);
                }

                else
                {
                    isSuccess = false;
                    message = "Project not found!";
                }
                return Json(new { success = $"{isSuccess}", message = $"{message}" });

            }
            catch (Exception)
            {

                throw;
            }
            
        }


        public IActionResult Details(int id)
        {
            var project = _projectInfoService.GetProjectInfo(id);
            if (project == null)
            {
                return NotFound("Project not found! ");
            }
            ViewBag.ProjectName = project.Name;

            var releases = _releaseService.GetAllReleases().Where(r => r.ProjectId == id).ToList();
            //var sprints = _sprintService.GetAllSprint().Where(s => s.ReleaseId == id).ToList();
            return View(project);
        }

    }
}
