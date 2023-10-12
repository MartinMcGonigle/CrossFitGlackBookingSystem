using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("WOD")]
    public class WOD
    {
        [Key]
        public long WODId { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }
    }
}