using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Repository
{
    public class WarmupRepository : RepositoryBase<Warmup>, IWarmupRepository
    {
        public WarmupRepository(ApplicationContext context) : base(context)
        {

        }
    }
}