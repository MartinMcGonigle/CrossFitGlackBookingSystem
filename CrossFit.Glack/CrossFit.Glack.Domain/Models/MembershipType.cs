using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("MembershipType")]
    public class MembershipType
    {
        [Key]
        public int MembershipTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Title")]
        public string MembershipTypeTitle { get; set; }

        [Display(Name = "Price")]
        public double MembershipTypePrice { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string MembershipTypeDescription { get; set; }

        public bool MembershipTypeActive { get; set; }
    }
}