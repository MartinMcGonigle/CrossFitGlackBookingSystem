using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Repository
{
    public class MembershipTypeRepository : RepositoryBase<MembershipType>, IMembershipTypeRepository
    {
        public MembershipTypeRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }
    }
}