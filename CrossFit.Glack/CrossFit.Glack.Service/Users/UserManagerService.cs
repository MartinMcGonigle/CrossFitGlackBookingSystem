using CrossFit.Glack.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrossFit.Glack.Service.Users
{
    public class UserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public SelectList GetCoachesSelectList()
        {
            var superUsers = _userManager.GetUsersInRoleAsync("SuperUser").Result;
            var staffUsers = _userManager.GetUsersInRoleAsync("Staff").Result;

            var users = superUsers.Union(staffUsers).Distinct().Select(user => new {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}"
            }).ToList();

            return new SelectList(users, "Id", "FullName");
        }
    }
}