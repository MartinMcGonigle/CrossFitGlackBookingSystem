using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("ClassRegistration")]
    public class ClassRegistration
    {
        [Key]
        public long ClassRegistrationId {  get; set; }

        public long? ClassId { get; set; }

        public string? UserId { get; set; }

        public DateTime RegistrationTime { get; set; }

        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}