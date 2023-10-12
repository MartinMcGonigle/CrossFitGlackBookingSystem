using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Wrapper;
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

        public UserController(
            UserManager<ApplicationUser> userManager,
            IRepositoryWrapper repository,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _repository = repository;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var data = _userManager.Users;
            foreach (var user in data)
            {
                user.UserRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            }

            return View(data);
        }

        [HttpGet("Resend-Verification-Link/{id}")]
        public async Task<IActionResult> ResendVerificationLink(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
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
    }
}