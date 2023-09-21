using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("MailServer")]
    public class MailServer
    {
        [Key]
        public int MailServerId { get; set; }

        [MaxLength(50)]
        public string MailServerName { get; set; }

        [MaxLength(50)]
        public string MailServerIP { get; set; }

        [MaxLength(100)]
        public string MailServerUserName { get; set; }

        [MaxLength(100)]
        public string MailServerPassword { get; set; }

        public int MailServerPort { get; set; }

        public bool Active { get; set; }
    }
}