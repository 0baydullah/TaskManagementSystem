using DataAccessLayer.Data;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class ProjectController : Controller
    {
        private readonly PMSDBContext _context;
        private readonly IWebHostEnvironment _env;

        public ProjectController(PMSDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {

            var projects = _context.ProjectInfo.ToList();
            return View(projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProjectInfoVM viewModel)
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

                if (viewModel.Files != null && viewModel.Files.Count > 0)
                {
                      foreach(var file in viewModel.Files)
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
                var project = new ProjectInfo
                {
                    Name = viewModel.Name,
                    Key = viewModel.Key,
                    Description = viewModel.Description,
                    StartDate = viewModel.StartDate,
                    EndDate = viewModel.EndDate,
                    CompanyName = viewModel.CompanyName,
                    ProjectOwnerId = viewModel.ProjectOwnerId,
                    Files = files
                };

                _context.ProjectInfo.Add(project);
                await _context.SaveChangesAsync();
                isSuccess = true;
                message = "Project created successfully!";
                

            }

            return Json(new { isSuccess, message });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var project = _context.ProjectInfo.FirstOrDefault(p => p.ProjectId == id);

            if (project == null)
            {
                return NotFound("Project not found! ");
            }

            else
            {
                var viewModel = new EditProjectInfoVM
                {
                    Name = project.Name,
                    Key = project.Key,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    CompanyName = project.CompanyName,
                    ProjectOwnerId = project.ProjectOwnerId,
                    ExistingFiles = project.Files ?? new List<string>()
                };
                return View(viewModel);

            }
        }

    }
}
