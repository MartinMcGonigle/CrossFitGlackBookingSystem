using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Interfaces
{
    public interface IClassRegistrationRepository : IRepositoryBase<ClassRegistration>
    {
        public IEnumerable<ClassRegistration> GetReservations(long id);
    }
}