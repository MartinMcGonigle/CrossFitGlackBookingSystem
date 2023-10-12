// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CrossFit.Glack.Staff.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "SuperUser, Staff")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public RegisterModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IRepositoryWrapper repositoryWrapper)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._logger = logger;
            this._emailSender = emailSender;
            this._roleManager = roleManager;
            this._configuration = configuration;
            this._repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Membership Type")]
            public int MembershipTypeId { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            var roles = _roleManager.Roles.ToList();
            var membershipTypes = this._repositoryWrapper.MembershipTypeRepository.FindAll().Where(x => x.MembershipTypeActive);

            ViewData["roles"] = roles;
            ViewData["membershipTypes"] = membershipTypes;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userRoleResult = await _userManager.AddToRoleAsync(user, Input.Role);
                    if (userRoleResult.Succeeded)
                    {
                        this._logger.LogInformation("User role added.");
                    }
                    else
                    {
                        this._logger.LogError($"Unable to add user {user} to the role {Input.Role}");
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    if (Input.Role == "Customer")
                    {
                        var membership = new Membership
                        {
                            MembershipActive = true,
                            MembershipCreationDate = DateTime.Now,
                            MembershipStartDate = DateTime.Now,
                            MembershipEndDate = DateTime.Now.AddMonths(1),
                            ApplicationUserId = user.Id,
                            MembershipTypeId = Input.MembershipTypeId
                        };

                        this._repositoryWrapper.MembershipRepository.Create(membership);
                        this._repositoryWrapper.Save();

                        var staff = this._configuration.GetSection("WebUrls").GetSection("Staff").Value;
                        var customer = this._configuration.GetSection("WebUrls").GetSection("Customer").Value;

                        callbackUrl = callbackUrl.Replace(staff, customer);
                    }

                    await _emailSender.SendEmailAsync(Input.Email, "CrossFit Glack Account Confirmation",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (returnUrl == "/")
                        return LocalRedirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            var roles = _roleManager.Roles.ToList();
            var membershipTypes = this._repositoryWrapper.MembershipTypeRepository.FindAll().Where(x => x.MembershipTypeActive);

            ViewData["roles"] = roles;
            ViewData["membershipTypes"] = membershipTypes;

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}