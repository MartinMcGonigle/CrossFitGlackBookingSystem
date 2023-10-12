using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("Class")]
    public class Class
    {
        [Key]
        public long ClassId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        public DateTime Date {  get; set; }

        [Required]
        public TimeSpan Time {  get; set; }

        [Required]
        [Display(Name = "Duration (Minutes)")]
        [Range(1, 120, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int DurationInMinutes { get; set; }

        [Required]
        [Display(Name = "Maximum Attendees")]
        [Range(1, 20, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int MaxAttendees { get; set; }

        [Required]
        [Display(Name = "Available Spots")]
        public int AvailableSpots { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        public string? SpecialRequirements { get; set; }

        public string InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public ApplicationUser User { get; set; }
    }
}