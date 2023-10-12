using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("Workout")]
    public class Workout
    {
        [Key]
        public long WorkoutId { get; set; }

        public DateTime Date {  get; set; }

        public long? WarmupId { get; set; }

        public long? StrengthId { get; set; }

        public long? WODId { get; set; }

        public bool Active { get; set; }

        [ForeignKey("WarmupId")]
        public Warmup? Warmup { get; set; }

        [ForeignKey("StrengthId")]
        public Strength? Strength { get; set; }

        [ForeignKey("WODId")]
        public WOD? WOD { get; set; }
    }
}