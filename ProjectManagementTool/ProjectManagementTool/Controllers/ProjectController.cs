﻿using System.Data;
using BusinessLogicLayer.IService;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using DataAccessLayer.StaticClass;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectInfoService _projectInfoService;
        private readonly UserManager<UserInfo> _userManager;
        private readonly IMemberService _memberService;
        private readonly IActivityService _activityService;
        private readonly ILog _log = LogManager.GetLogger(typeof(ProjectController));

        public ProjectController(IProjectInfoService projectInfoService,
            UserManager<UserInfo> userManager, IMemberService memberService, IActivityService activityService)
        {
            _projectInfoService = projectInfoService;
            _userManager = userManager;
            _memberService = memberService;
            _activityService = activityService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var projects = _projectInfoService.GetAllProjectInfo(user?.Email ?? "");
                var data = projects.Select(p => new ProjectInfoVM
                {
                    ProjectId = p.ProjectId,
                    Name = p.Name,
                    Key = p.Key,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    CompanyName = p.CompanyName,
                    ProjectOwnerId = p.ProjectOwnerId,
                    MemberList = _memberService.GetAllMember().Where(m => m.ProjectId == p.ProjectId).ToDictionary(member => member.MemberId, member => member.Name),
                    //IsOwner = _memberService.GetAllMember().Where( m => (m.ProjectId == p.ProjectId) && (m.Email == user.Email)).FirstOrDefault().Role == RoleHelper.OwnerRoleName ? true : false,
                    IsOwner = _projectInfoService.IsOwner(p.ProjectId, user.Email),
                }).ToList();

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
        public IActionResult GetAllProjectTask( int projectId)
        {
            try
            {
                var project = _projectInfoService.GetProjectInfo(projectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;

                var tasks = _projectInfoService.GetAllTaskByProject(projectId);

                return View(tasks);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }

        }

        [HttpGet]
        public IActionResult GetAllProjectBug( int projectId)
        {
            try
            {
                var project = _projectInfoService.GetProjectInfo(projectId);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;

                var bugs = _projectInfoService.GetAllBugByProject(projectId);

                return View(bugs);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
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

                return Json(new { success = isSuccess, message });
            }

            try
            {

                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    isSuccess = false;
                    message = "User not found!";

                    return Json(new { success = isSuccess, message });
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
                    var roleName = RoleHelper.OwnerRoleName;
                    await _userManager.AddToRoleAsync(user, roleName);
                    isSuccess = true;
                    message = "Project created successfully!";
                    _log.Info(message);
                }

                return Json(new { success = isSuccess, message });
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
                ViewBag.ProjectId = id;
                ViewBag.ProjectKey = project.Key;

                return View(model);

            }
            catch (Exception ex)
            {

                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
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
                return Json(new { success = isSuccess, message });
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                var response = _projectInfoService.UpdateProjectInfo(model, user);
                
                if ( response == true )
                {
                    isSuccess = true;
                    message = "Project updated successfully!";
                    _log.Info(message);
                }
                else
                {
                    isSuccess = false;
                    message = "Project already exist!";
                    _log.Info(message);
                }
               
                return Json(new { success = isSuccess, message });
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
            var message = "Project deleted successfully!";
            try
            {
                var response = _projectInfoService.GetProjectInfo(id);
                _projectInfoService.DeleteProjectInfo(response);
                _log.Info(message);

                return Json(new { success = isSuccess, message });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var project = await _projectInfoService.GetProjectInfoDetails(id);
                ViewBag.ProjectId = id;
                ViewBag.ProjectKey = project.Key;

                return View(project);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }

        }

        public async Task<IActionResult> UpdateOwner(int ownerId, int projectId)
        {
            var isSuccess = true;
            var message = "Project Owner updated successfully!";
            try
            {
                var response = await _projectInfoService.UpdateProjectOwner(ownerId, projectId);
                if (response == true)
                {
                    _log.Info(message);
                }
                else
                {
                    isSuccess = false;
                    message = "Hey! You are already in owner role";
                    _log.Warn(message);
                }

                return Json(new { success = isSuccess, message });
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

