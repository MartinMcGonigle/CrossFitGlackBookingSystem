using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrossFit.Glack.Repository.Repository
{
    public class WorkoutRepository : RepositoryBase<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(ApplicationContext context) : base(context)
        {

        }

        public Workout GetWorkoutById(long id)
        {
            return Context.Workouts.Include(x => x.Warmup)
                .Include(x => x.Strength)
                .Include(x => x.WOD)
                .FirstOrDefault(x => x.WorkoutId == id);
        }

        public Workout GetWorkoutByDate(DateTime dateTime)
        {
            return Context.Workouts.Include(x => x.Warmup)
                .Include(x => x.Strength)
                .Include(x => x.WOD)
                .FirstOrDefault(x => x.Date == dateTime && x.Active);
        }
    }
}