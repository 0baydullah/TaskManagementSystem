using BusinessLogicLayer.IService;
using DataAccessLayer.Data;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using DataAccessLayer.Repository;
using log4net;
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
        private readonly ILog _log= LogManager.GetLogger(typeof(SprintRepo));

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
                ViewBag.Error = "Opps! Exception Occurred: " + ex.Message;
                _log.Error(ViewBag.Error);
                TempData["Error"] = ex.Message;
                
                return RedirectToAction("Exception","Error");
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

                    return Json(new { success = $"{isSuccess}", message = $"{message}" });
                }

                var response = _projectInfoService.AddProjectInfo(model, user);
                if (response == false)
                {
                    isSuccess = false;
                    message = "Project already exist!";
                    _log.Info(message);
                }
                else
                {
                    isSuccess = true;
                    message = "Project created successfully!";
                    _log.Info(message);
                }
                
                return Json(new { success = $"{isSuccess}", message = $"{message}" });
            }
            catch (Exception ex )
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
                var project = _projectInfoService.GetProjectInfo(id);
                
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
            catch (Exception ex)
            {

                ViewBag.Error = "Opps! Exception Occurred: " + ex.Message;
                _log.Error(ViewBag.Error);

                return View();
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
                _projectInfoService.UpdateProjectInfo(model);
                isSuccess = true;
                message = "Project updated successfully!";
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


        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool isSuccess = true;
            var message = "Project deleted successfully!";
            try
            {
                var response = _projectInfoService.GetProjectInfo(id);
                _projectInfoService.DeleteProjectInfo(response);
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


        public IActionResult Details(int id)
        {
            try
            {
                var project = _projectInfoService.GetProjectInfo(id);
                ViewBag.ProjectName = project.Name;

                return View(project);

            }
            catch (Exception ex)
            {
                ViewBag.Error = "Opps! Exception Occurred: " + ex.Message;
                _log.Error(ViewBag.Error);

                return View();
            }
            
        }

    }
}
