using CrossFit.Glack.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrossFit.Glack.Domain.Context
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<MailServer> MailServers { get; set; }

        public DbSet<MembershipType> MembershipTypes { get; set; }

        public DbSet<Membership> Memberships { get; set; }
    }
}