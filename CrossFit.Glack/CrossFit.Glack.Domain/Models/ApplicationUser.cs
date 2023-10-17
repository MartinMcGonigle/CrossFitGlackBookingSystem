using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // public ICollection<Membership> Memberships { get; set; }
        
        [NotMapped]
        public IList<string>? UserRoles { get; set; }
    }
}