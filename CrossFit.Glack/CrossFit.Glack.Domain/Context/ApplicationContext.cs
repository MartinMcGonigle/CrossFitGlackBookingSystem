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

        public DbSet<Workout> Workouts { get; set; }

        public DbSet<Warmup> Warmups { get; set; }

        public DbSet<Strength> Strengths { get; set; }

        public DbSet<WOD> WODs { get; set; }
        
        public DbSet<Class> Classes { get; set; }
        
        public DbSet<ClassRegistration> ClassRegistrations { get; set; }
    }
}