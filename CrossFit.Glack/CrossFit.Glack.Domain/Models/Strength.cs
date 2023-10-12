using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("Strength")]
    public class Strength
    {
        [Key]
        public long StrengthId { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }
    }
}