using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossFit.Glack.Domain.Models
{
    [Table("NewsFeed")]
    public class NewsFeed
    {
        [Key]
        public long NewsFeedId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Heading")]
        [Required]
        public string NewsFeedName { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string NewFeedMessage { get; set; }

        public string UserCreated { get; set; }

        public DateTime DateCreated { get; set; }

        [Display(Name = "Please select users who can not see this notification")]
        public string? UserGrant {  get; set; }

        [NotMapped]
        public List<string> UserGrantAcess {  get; set; }
    }
}