using BusinessLogicLayer.IService;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IRoleService _roleService;
        public RoleController(RoleManager<IdentityRole<int>> roleManager, IRoleService roleService) 
        {
            _roleManager = roleManager;
            _roleService = roleService;
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
            var model = _roleService.GetAllRole();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteRole(id);
            return Ok();
        }
    }
}
