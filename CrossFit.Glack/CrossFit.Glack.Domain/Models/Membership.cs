using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("Membership")]
    public class Membership
    {
        [Key]
        public long MembershipId { get; set; }

        [Required]
        public bool MembershipActive { get; set; }

        [Required]
        public DateTime MembershipCreationDate { get; set; }

        [Required]
        public DateTime MembershipStartDate { get; set; }

        [Required]
        public DateTime MembershipEndDate { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        [Required]
        public bool MembershipAutoRenew { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("MembershipTypeId")]
        public MembershipType MembershipType { get; set; }
    }
}