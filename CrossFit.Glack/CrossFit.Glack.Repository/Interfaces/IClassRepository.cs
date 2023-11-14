using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Domain.OtherModels;

namespace CrossFit.Glack.Repository.Interfaces
{
    public interface IClassRepository : IRepositoryBase<Class>
    {
        public IEnumerable<ClassViewModel> GetTodaysClasses(DateTime dateTime);

        public IEnumerable<ClassViewModel> GetClassesInFuture(DateTime dateTime);

        public IEnumerable<Class> GetTodaysClassesCustomer(DateTime dateTime, string userId);

        public IEnumerable<Class> GetClassesInFutureCustomer(DateTime dateTime, string userId);
    }
}