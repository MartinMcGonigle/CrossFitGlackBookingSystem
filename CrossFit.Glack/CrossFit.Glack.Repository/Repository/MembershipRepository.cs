using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Repository
{
    public class MembershipRepository : RepositoryBase<Membership>, IMembershipRepository
    {
        public MembershipRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }
    }
}
