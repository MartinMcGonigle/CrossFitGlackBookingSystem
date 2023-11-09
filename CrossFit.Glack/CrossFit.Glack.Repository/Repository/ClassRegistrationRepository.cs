using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace CrossFit.Glack.Repository.Repository
{
    public class ClassRegistrationRepository : RepositoryBase<ClassRegistration>, IClassRegistrationRepository
    {
        public ClassRegistrationRepository(ApplicationContext context) : base (context)
        {

        }

        public IEnumerable<ClassRegistration> GetReservations(long id)
        {
            return Context.ClassRegistrations
            .Include(x => x.Class)
            .Include(x => x.User)
            .Where(x => x.ClassId == id).ToList();
        }
    }
}