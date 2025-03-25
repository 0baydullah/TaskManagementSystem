using BusinessLogicLayer.IService;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IRoleService _roleService;
        private readonly IMemberService _memberService;
        public RoleController(RoleManager<IdentityRole<int>> roleManager, 
            IRoleService roleService, IMemberService memberService) 
        {
            _roleManager = roleManager;
            _roleService = roleService;
            _memberService = memberService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleVM roleModel) 
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { success = false, errors });
            }
            try
            {

                bool roleExists = await _roleManager.RoleExistsAsync(roleModel?.RoleName);

                if (roleExists == true)
                {
                    return BadRequest(new { success = false, errors = new List<string> { "Role already exist" } });
                }
                else
                {
                    IdentityRole<int> identityRole = new IdentityRole<int>
                    {
                        Name = roleModel?.RoleName
                    };

                    IdentityResult result = await _roleManager.CreateAsync(identityRole);

                    if (result.Succeeded)
                    {
                        return Ok(new { success = true, redirectUrl = Url.Action("GetAll", "Role") });
                    }
                    else
                    {
                        return BadRequest(new { success = false, errors = new List<string> { "Failed" } });
                    }
                }
            }
            catch (Exception)
            {
                return View(roleModel);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var model = _roleService.GetAllRole();
                return View(model);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var role = _roleService.GetRoleById(id);
                return Json(new
                {
                    success = true,
                    data = role
                });
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleVM model)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { success = false, errors });
            }
            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
                if(role == null)
                {
                    return BadRequest(new { success = false, errors = new List<string> { "Role can not be found" } });
                }
                else
                {
                    role.Name = model.RoleName;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return Ok(new { success = true, redirectUrl = Url.Action("GetAll", "Role") });
                    }
                    else
                    {
                        return BadRequest(new { success = false, errors = new List<string> { "Failed" } });
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                var member  = _memberService.GetAllMember().Where(m => m.Role == role.Name).FirstOrDefault();
                
                if (role == null || member!= null)
                {
                    return BadRequest(new { success = false, error = "Role can not be deleted" });
                }
                else
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return Ok(new { success = true, redirectUrl = Url.Action("GetAll", "Role") });
                    }
                    else
                    {
                        return BadRequest(new { success = false, error = "Delete Failed"});
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
