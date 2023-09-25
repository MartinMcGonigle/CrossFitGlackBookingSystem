using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CrossFit.Glack.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public ICollection<Membership> Memberships { get; set; }
    }
}