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
        [Required]
        [Range(1, double.MaxValue)]
        public double MembershipTypePrice { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string MembershipTypeDescription { get; set; }

        [Required]
        [Display(Name = "Membership Active")]
        public bool MembershipTypeActive { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "The type of the membership must be selected")]
        public int Type {  get; set; }

        [Display(Name = "Number of Classes")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Classes must be greater than 0")]
        public int? NumberOfClasses { get; set; }

        [Display(Name = "Number of Months")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Months must be greater than 0")]
        public int? NumberOfMonths { get; set; }
    }
}