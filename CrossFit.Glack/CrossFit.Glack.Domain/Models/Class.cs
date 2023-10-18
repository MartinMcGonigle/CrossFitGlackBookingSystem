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
        public DateTime Date {  get; set; }

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

        [Display(Name = "Coach")]
        public string? InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public ApplicationUser? User { get; set; }

        public bool Active { get; set; }

        [Required]
        public DateTime DateTimeEnd { get; set; }

        [NotMapped]
        public int? SlotsTaken { get; set; }

        [NotMapped]
        public string? InstructorName { get; set; }
    }
}