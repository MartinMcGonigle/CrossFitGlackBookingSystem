using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("Warmup")]
    public class Warmup
    {
        [Key]
        public long WarmupId { get; set; }

        public string Description { get; set; }

        public int Order {  get; set; }
    }
}