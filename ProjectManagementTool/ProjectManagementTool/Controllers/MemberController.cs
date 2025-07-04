﻿using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IRoleService _roleService;
        private readonly IProjectInfoService _projectInfoService;
        private readonly UserManager<UserInfo> _userManager;
        private readonly ITasksService _tasksService;
        private readonly IBugService _bugService;
        private readonly ILog _log = LogManager.GetLogger(typeof(MemberController));
        public MemberController(IMemberService memberService, IRoleService roleService, IProjectInfoService projectInfoService
            , UserManager<UserInfo> userManager, ITasksService tasksService, IBugService bugService)
        {
            _memberService = memberService;
            _roleService = roleService;
            _projectInfoService = projectInfoService;
            _userManager = userManager;
            _tasksService = tasksService;
            _bugService = bugService;
        }

        [HttpGet]
        public IActionResult Index(int projectId)
        {
            try
            {
                var project = _projectInfoService.GetProjectInfo(projectId);
                var members = _memberService.GetAllMember().Where(m => m.ProjectId == projectId).ToList();
                ViewBag.ProjectId = projectId;
                ViewBag.ProjectKey = project.Key;

                return View(members);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }

        }

        [HttpGet]

        public IActionResult GetAllUser()
        {
            try
            {
                var users = _userManager.Users.ToList();
                var allUsers = _memberService.GetAllUser(users);

                return View(allUsers);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserInfoVM model)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return BadRequest(new { success = false, errors });
            }
            try
            {
                var user = new UserInfo
                {
                    Pin = model.Pin,
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var normalizedEmail = user.Email.ToUpperInvariant();
                user.NormalizedEmail = normalizedEmail;

                var existEmployeeId = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == model.Email || e.Pin == model.Pin);

                if (existEmployeeId != null)
                {
                    return BadRequest(new { success = false, errors = new List<string> { "This email or pin is already registered." } });
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded == true)
                {
                    return RedirectToAction("GetAllUser", "Member");
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new { success = false, errors });
                }
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
                var roles = _roleService.GetAllRole();
                var ownerRole = roles.FirstOrDefault(x => x.RoleName == "Owner");
                if (ownerRole != null)
                {
                    roles.Remove(ownerRole);
                }
                var project = _projectInfoService.GetProjectInfo(projectId);
                ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
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
        public async Task<IActionResult> Create(Member model)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted!";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    message += error + " ";
                    Console.WriteLine(error);
                }

                return Json(new { success = isSuccess, message });
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var roleName = _roleService.GetRoleById(model.RoleId).RoleName;
                    await _userManager.AddToRoleAsync(user, roleName);
                    var response = _memberService.AddMember(model);
                    if (response == true)
                    {
                        isSuccess = true;
                        message = "Member added successfully!";
                        _log.Info(message);

                    }
                    else
                    {
                        isSuccess = false;
                        message = "Member already exist!";
                        _log.Info(message);

                    }

                }
                else
                {
                    isSuccess = false;
                    message = "User not found!";
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
                var member = _memberService.GetMember(id);
                var project = _projectInfoService.GetProjectInfo(member.ProjectId);

                var model = new MemberVM
                {
                    MemberId = member.MemberId,
                    Email = member.Email,
                    RoleId = member.RoleId,
                };
                var roles = _roleService.GetAllRole();
                var ownerRole = roles.FirstOrDefault(x => x.RoleName == "Owner");
                if (ownerRole != null)
                {
                    roles.Remove(ownerRole);
                }
                ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName", member.RoleId);
                ViewBag.ProjectId = member.ProjectId;
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
        public async Task<IActionResult> Edit(MemberVM model, int id)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted! ";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    message += error + " ";
                    Console.WriteLine(error);
                }

                return Json(new { success = isSuccess, message });
            }
            try
            {

                var response = await _memberService.UpdateMember(id, model);
                if (response == true)
                {
                    isSuccess = true;
                    message = "Member updated successfully!";
                    _log.Info(message);
                }
                else
                {
                    isSuccess = false;
                    message = "Member not Updated";
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
            string message = "Member deleted successfully!";
            try
            {
                var member = _memberService.GetMember(id);
                var tasks = _tasksService.GetAllTasks().Where( t => t.AssignMembersId == id).FirstOrDefault();
                var bugs = _bugService.GetAllBug().Where(t => t.AssignMembersId == id).FirstOrDefault();
                
                if( tasks == null && bugs == null)
                {
                    _memberService.DeleteMember(member);
                    _log.Warn(message);
                }
                else
                {
                    isSuccess = false;
                    message = "Member cannot be deleted!";
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

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var member = _memberService.GetMemberDetails(id);
                var project = _projectInfoService.GetProjectInfo(member.ProjectId);
                ViewBag.ProjectId = member.ProjectId;
                ViewBag.ProjectKey = project.Key;
                
                return View(member);
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
