using CrossFit.Glack.Domain.Models;

namespace CrossFit.Glack.Repository.Interfaces
{
    public interface IWorkoutRepository : IRepositoryBase<Workout>
    {
        public Workout GetWorkoutById(long id);

        public Workout GetWorkoutByDate(DateTime dateTime);
    }
}