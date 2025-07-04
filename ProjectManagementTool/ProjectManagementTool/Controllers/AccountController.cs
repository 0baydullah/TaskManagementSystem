﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using BusinessLogicLayer.IService;
using log4net;

namespace ProjectManagementTool.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly SignInManager<UserInfo> _signInManager;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILog _log = LogManager.GetLogger(typeof(AccountController));

        public AccountController(UserManager<UserInfo> userManager, SignInManager<UserInfo> signInManager,
            IEmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSenderService = emailSenderService;

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register( UserInfoVM model)
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
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
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

                return Ok(new { success = true, redirectUrl = Url.Action("index", "Project") });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        await SendForgotPasswordEmail(user.Email, user);
                        return RedirectToAction("ForgotPasswordConfirmation", "Account");
                    }

                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        private async Task SendForgotPasswordEmail(string? email, UserInfo? user)
        {
            try
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetLink = Url.Action("ResetPassword", "Account",
                    new { Email = email, Token = token }, protocol: HttpContext.Request.Scheme);
                var safeLink = HtmlEncoder.Default.Encode(passwordResetLink);
                var subject = "Reset Your Password";

                var messageBody = $@"
            <div style=""font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #333; line-height: 1.5; padding: 20px;"">
                <h2 style=""color: #007bff; text-align: center;"">Password Reset Request</h2>
        
        
                <p>You received a request to reset your password for your <strong>Project Management Tool</strong> account. If you made this request, please click the button below to reset your password:</p>
        
                <div style=""text-align: center; margin: 20px 0;"">
                    <a href=""{safeLink}"" 
                       style=""background-color: #007bff; color: #fff; padding: 10px 20px; text-decoration: none; font-weight: bold; border-radius: 5px; display: inline-block;"">
                        Reset Password
                    </a>
                </div>
        
                <p>If the button above doesn’t work, copy and paste the following URL into your browser:</p>
                <p style=""background-color: #f8f9fa; padding: 10px; border: 1px solid #ddd; border-radius: 5px;"">
                    <a href=""{safeLink}"" style=""color: #007bff; text-decoration: none;"">{safeLink}</a>
                </p>
        
                <p>If you did not request to reset your password, please ignore this email or contact support if you have concerns.</p>
        
                <p style=""margin-top: 30px;"">Thank you,<br />Project Management Tool</p>
            </div>";

                await _emailSenderService.SendEmailAsync(email, subject, messageBody, IsBodyHtml: true);

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(string Token, string Email)
        {
            try
            {
                if (Token == null || Email == null)
                {
                    ViewBag.ErrorTitle = "Invalid Password Reset Token";
                    ViewBag.ErrorMessage = "The Link is Expired or Invalid";
                    return View("Error");
                }
                else
                {
                    ResetPasswordVM model = new ResetPasswordVM()
                    {
                        Token = Token,
                        Email = Email
                    };

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {
                            await _signInManager.SignOutAsync();
                            return RedirectToAction("ResetPasswordConfirmation", "Account");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }

                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Account");
                    }

                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ChangePasswordConfirmation", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

    }
}
