using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Repository
{
    public class StrengthRepository : RepositoryBase<Strength>, IStrengthRepository
    {
        public StrengthRepository(ApplicationContext context) : base(context)
        {

        }
    }
}