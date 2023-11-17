using CrossFit.Glack.Domain.Models;

namespace CrossFit.Glack.Repository.Interfaces
{
    public interface IMembershipRepository : IRepositoryBase<Membership>
    {
        public Membership GetUserMembership(string id);
    }
}