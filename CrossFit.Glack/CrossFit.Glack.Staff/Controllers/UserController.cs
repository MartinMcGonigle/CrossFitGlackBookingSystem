using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Wrapper;
using CrossFit.Glack.Staff.ViewResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace CrossFit.Glack.Staff.Controllers
{
    [Authorize(Roles = "SuperUser, Staff")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepositoryWrapper _repository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
        private readonly string logPrefix = "Ctlr|User";

        public UserController(
            UserManager<ApplicationUser> userManager,
            IRepositoryWrapper repository,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            IConfiguration configuration,
            ILogger<UserController> logger)
        {
            _userManager = userManager;
            _repository = repository;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation($"{logPrefix} - Displaying all users");
            var users = _userManager.Users;

            foreach (var user in users)
            {
                user.UserRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            }

            return View(users);
        }

        [HttpGet("Resend-Verification-Link/{id}")]
        public async Task<IActionResult> ResendVerificationLink(string id)
        {
            _logger.LogInformation($"{logPrefix} - resending verification link to user with id: {id}");
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                _logger.LogError($"{logPrefix} - failed to find user in the database with id: {id}");
                return new NotFound();
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = "" },
                protocol: Request.Scheme);

            user.UserRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            if (user.UserRoles.Contains("Customer"))
            {
                var staff = _configuration.GetSection("WebUrls").GetSection("Staff").Value;
                var customer = _configuration.GetSection("WebUrls").GetSection("Customer").Value;

                callbackUrl = callbackUrl.Replace(staff, customer);
            }

            try
            {
                await _emailSender.SendEmailAsync(user.Email, "CrossFit Glack Account Confirmation",
                    $"Hi {user.FirstName} {user.LastName}, <p>Welcome to CrossFit Glack.</p> <p> Please confirm your CrossFit Glack account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.</p><p>Thank you <br />CrossFit Glack</p>");

                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new NotFound();
            
            await _userManager.DeleteAsync(user);
            
            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit (string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ApplicationUser model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                _logger.LogError($"{logPrefix} - failed to find user in the database with id: {id}");
                return new NotFound();
            }
                

            if (ModelState.IsValid)
            {
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Successfully updated user account with id: {id}");
                    return RedirectToAction(nameof(this.Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}