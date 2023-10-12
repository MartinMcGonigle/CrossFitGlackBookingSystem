using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Repository
{
    public class ClassRegistrationRepository : RepositoryBase<ClassRegistration>, IClassRegistrationRepository
    {
        public ClassRegistrationRepository(ApplicationContext context) : base (context)
        {

        }
    }
}