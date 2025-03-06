using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementTool.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly SignInManager<UserInfo> _signInManager;


        public AccountController(UserManager<UserInfo> userManager, SignInManager<UserInfo> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
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

                // Store user data in AspNetUsers database table
                var result = await _userManager.CreateAsync(user, model.Password);

                // If user is successfully created, sign-in the user and redirect to index action of Account controller
                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, "User");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
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

        public IActionResult Login()
        {
            // after implementing role here check to user previously signed in or not and do not sign in from other browser
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return BadRequest(new { success = false, errors });
            }

            try
            {

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);

                if (result.Succeeded == false)
                {
                    return BadRequest(new { success = false, errors = new List<string> { "Invalid Email or Password" } });
                }

                TempData["welcomeMessage"] = "wow you are logged in the system";
                return Ok(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            catch (Exception)
            {
                return View();
            }
        }

    }
}
