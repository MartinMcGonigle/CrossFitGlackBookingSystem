using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Repository
{
    public class WODRepository : RepositoryBase<WOD>, IWODRepository
    {
        public WODRepository(ApplicationContext context) : base(context)
        {

        }
    }
}