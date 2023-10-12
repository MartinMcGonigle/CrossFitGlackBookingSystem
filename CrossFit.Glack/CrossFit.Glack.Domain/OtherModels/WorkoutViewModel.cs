using System.ComponentModel;

namespace CrossFit.Glack.Domain.OtherModels
{
    public class WorkoutViewModel
    {
        public long WorkoutId { get; set; }

        public DateTime Date { get; set; }

        [DisplayName("Warmup")]
        public string? WarmupDescription { get; set; }

        [DisplayName("Strength")]
        public string? StrengthDescription { get; set; }

        [DisplayName("WOD")]
        public string? WODDescription { get; set; }

        public bool Active { get; set; }
    }
}