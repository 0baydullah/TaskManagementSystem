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
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<UserInfo> _userManager;

        public ProjectController(IWebHostEnvironment env, IProjectInfoService projectInfoService, 
            UserManager<UserInfo> userManager)
        {
            _env = env;
            _projectInfoService = projectInfoService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var projects = _projectInfoService.GetAllProjectInfo();
            return View(projects);
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
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select( e => e.ErrorMessage);

                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                    message += error + " ";
                }
            }

            else
            {
                var files = new List<string>();

                if (model.Files != null && model.Files.Count > 0)
                {
                      foreach(var file in model.Files)
                      {
                        string folder = Path.Combine(_env.WebRootPath, "files");
                        if (Directory.Exists(folder) == false)
                        {
                            Directory.CreateDirectory(folder);
                        }

                        string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(folder, fileName);
                        await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
                        files.Add(fileName);
                      }
                }
                var user = await _userManager.GetUserAsync(User);
                var project = new ProjectInfo
                {
                    Name = model.Name,
                    Key = model.Key,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    CompanyName = model.CompanyName,
                    ProjectOwnerId = user.Id,
                    Files = files
                };

                _projectInfoService.AddProjectInfo(project);
                isSuccess = true;
                message = "Project created successfully!";
                

            }

            return Json(new { isSuccess, message });
        }

        [HttpGet]
        public IActionResult Edit(int id)
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
                    ExistingFiles = project.Files ?? new List<string>()
                };
                
                return View(model);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectInfoVM model, int id)
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
            }

            else
            {
                var project = _projectInfoService.GetProjectInfo(id);
                if (project == null)
                {
                    isSuccess = false;
                    message = "Student not found!";
                    return Json(new { success = $"{isSuccess}", message = $"{message}" });
                }

                var files = project.Files ?? new List<string>();


                if (model.Files != null && model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {
                        string folder = Path.Combine(_env.WebRootPath, "files");
                        if (Directory.Exists(folder) == false)
                        {
                            Directory.CreateDirectory(folder);
                        }

                        string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(folder, fileName);
                        await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
                        files.Add(fileName);
                    }
                }


                project.Name = model.Name;
                project.Key = model.Key;
                project.Description = model.Description;
                project.StartDate = model.StartDate;
                project.EndDate = model.EndDate;
                project.CompanyName = model.CompanyName;
                project.ProjectOwnerId = model.ProjectOwnerId;
                
                _projectInfoService.UpdateProjectInfo(project);
                isSuccess = true;
                message = "Data updated successfully!";
            }

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = _projectInfoService.GetProjectInfo(id);

            if (project != null)
            {
                _projectInfoService.DeleteProjectInfo(project);
            }
            return Json(new { success = "true", message = "Project deleted successfully!" });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(string file, int id)
        {
            bool isSuccess = false;
            var message = "Invalid Data!";

            if (string.IsNullOrEmpty(file))
            {
                isSuccess = false;
                message = "Photo URL Not Found!";
            }

            else
            {
                var filePath = Path.Combine(_env.WebRootPath, file.TrimStart('/'));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                var project = _projectInfoService.GetProjectInfo(id);

                if (project == null)
                {
                    isSuccess = false;
                    message = "Student not found!";
                }


                if (project.Files != null && project.Files.Contains(file))
                {
                    project.Files.Remove(file);
                    _projectInfoService.UpdateProjectInfo(project);
                    isSuccess = true;
                    message = "Photo deleted successfully!";

                }

            }
            return Json(new { success = $"{isSuccess}", message = $"{message}" });

        }

    }
}
