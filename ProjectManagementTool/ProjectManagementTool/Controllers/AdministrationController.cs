using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using DataAccessLayer.StaticClass;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjectManagementTool.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;


        public AdministrationController(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoleClaims(int RoleId)
        {
            
            var role = await _roleManager.FindByIdAsync(RoleId.ToString());

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {RoleId} cannot be found";
                return View("NotFound");
            }

            ViewBag.RoleName = role.Name;

            var model = new RoleClaimsVM
            {
                RoleId = RoleId
            };

            var existingRoleClaims = await _roleManager.GetClaimsAsync(role);

            foreach (Claim claim in ClaimStore.GetAllClaims())
            {
                RoleClaim roleClaim = new RoleClaim
                {
                    ClaimType = claim.Type
                };

                if (existingRoleClaims.Any(c => c.Type == claim.Type))
                {
                    roleClaim.IsSelected = true;
                }

                model.Claims.Add(roleClaim);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoleClaims(RoleClaimsVM model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.RoleId} cannot be found";
                return View("NotFound");
            }

            var claims = await _roleManager.GetClaimsAsync(role);

            for (int i = 0; i < model.Claims.Count; i++)
            {
                Claim claim = new Claim(model.Claims[i].ClaimType, model.Claims[i].ClaimType);

                IdentityResult? result;

                if (model.Claims[i].IsSelected && !(claims.Any(c => c.Type == claim.Type)))
                {
                    //result = await _userManager.AddToRoleAsync(user, role.Name);
                    result = await _roleManager.AddClaimAsync(role, claim);
                }
                else if (!model.Claims[i].IsSelected && claims.Any(c => c.Type == claim.Type))
                {
                    result = await _roleManager.RemoveClaimAsync(role, claim);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Claims.Count - 1))
                        continue;
                    else
                        return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Cannot add or removed selected claims to role");
                    return View(model);
                }
            }
            return View(model);
        }

    }  
}
