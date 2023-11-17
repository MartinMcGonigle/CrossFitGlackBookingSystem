using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrossFit.Glack.Repository.Repository
{
    public class MembershipRepository : RepositoryBase<Membership>, IMembershipRepository
    {
        public MembershipRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            
        }

        public Membership GetUserMembership(string id)
        {
            return Context.Memberships
                .Include(x => x.ApplicationUser)
                .Include(x => x.MembershipType)
                .FirstOrDefault(x => x.ApplicationUserId == id);
        }
    }
}