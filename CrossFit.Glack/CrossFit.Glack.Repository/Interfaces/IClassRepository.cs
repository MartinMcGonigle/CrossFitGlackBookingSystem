using CrossFit.Glack.Domain.Models;

namespace CrossFit.Glack.Repository.Interfaces
{
    public interface IClassRepository : IRepositoryBase<Class>
    {
        public IEnumerable<Class> GetTodaysClasses(DateTime dateTime);

        public IEnumerable<Class> GetClassesInFuture(DateTime dateTime);

        public IEnumerable<Class> GetTodaysClassesCustomer(DateTime dateTime, string userId);

        public IEnumerable<Class> GetClassesInFutureCustomer(DateTime dateTime, string userId);
    }
}