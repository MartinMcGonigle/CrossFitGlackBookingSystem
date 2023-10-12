using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Repository
{
    public class ClassRepository : RepositoryBase<Class>, IClassRepository
    {
        public ClassRepository(ApplicationContext context) : base(context)
        {

        }
    }
}