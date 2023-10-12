using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Wrapper
{
    public interface IRepositoryWrapper
    {
        IMailServerRepository MailServerRepository { get; }

        IMembershipTypeRepository MembershipTypeRepository { get; }

        IMembershipRepository MembershipRepository { get; }

        IStrengthRepository StrengthRepository { get; }

        IWarmupRepository WarmupRepository { get; }

        IWODRepository WODRepository { get; }

        IWorkoutRepository WorkoutRepository { get; }

        IClassRepository ClassRepository { get; }

        IClassRegistrationRepository ClassRegistrationRepository { get; }

        void Save();
    }
}