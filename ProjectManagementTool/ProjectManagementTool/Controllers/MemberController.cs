using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
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
        public MemberController(IMemberService memberService, IRoleService roleService, IProjectInfoService projectInfoService
            , UserManager<UserInfo> userManager)
        {
            _memberService = memberService;
            _roleService = roleService;
            _projectInfoService = projectInfoService;
            _userManager = userManager;
        }
       
        [HttpGet]
        public IActionResult Index(int projectId)
        {
            var members = _memberService.GetAllMember().Where(m => m.ProjectId == projectId).ToList();
            ViewBag.ProjectId = projectId;
            return View(members);
        }

        [HttpGet]

        public IActionResult GetAllUser()
        {
            var users = _userManager.Users.ToList();

            var allUsers = _memberService.GetAllUser(users);


            return View(allUsers);
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
            catch (Exception)
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Create(int projectId)
        {
            var roles = _roleService.GetAllRole();
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");

            ViewData["ProjectId"] = projectId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Member model)
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

            }
            else
            {
                if (_memberService.GetUserByEmail(model.Email) == null)
                {
                    isSuccess = false;
                    message = "User is not exists!";
                    return Json(new { success = $"{isSuccess}", message = $"{message}" });
                }
                else
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var roleName = _roleService.GetRoleById(model.RoleId).RoleName;
                        await _userManager.AddToRoleAsync(user, roleName);
                        _memberService.AddMember(model);
                        isSuccess = true;
                        message = "Member added successfully!";
                    }
                }
            }

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _memberService.GetMember(id);
            if (member == null)
            {
                return NotFound("Member not found!");
            }

            var model = new MemberVM
            {
                MemberId = member.MemberId,
                Email = member.Email,
                RoleId = member.RoleId,
                ProjectId = member.ProjectId
            };
            var roles = _roleService.GetAllRole();
            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName", member.RoleId);
            
            var projects = _projectInfoService.GetAllProjectInfo();
            ViewData["ProjectId"] = new SelectList(projects, "ProjectId", "Name", member.ProjectId);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit( MemberVM model, int id)
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

            }

            else
            {
                var member = _memberService.GetMember(id);
                if (member == null)
                {
                    return NotFound("Member not found!");
                }
                member.Email = model.Email;
                member.RoleId = model.RoleId;
                member.ProjectId = model.ProjectId;
                _memberService.UpdateMember(member);
                isSuccess = true;
                message = "Member updated successfully!";
            }

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
        }

        [HttpPost]
        public IActionResult Delete(int id )
        {
            var member = _memberService.GetMember(id);

            if (member == null)
            {
                return NotFound("Member not found!");
            }

            _memberService.DeleteMember(member);

            return Json(new { success = true , message = "Member deleted successfully!"});

        }
    }
}
