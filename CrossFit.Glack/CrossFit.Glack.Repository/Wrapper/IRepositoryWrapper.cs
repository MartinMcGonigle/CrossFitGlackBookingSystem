using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Wrapper
{
    public interface IRepositoryWrapper
    {
        IMailServerRepository MailServerRepository { get; }

        IMembershipTypeRepository MembershipTypeRepository { get; }

        IMembershipRepository MembershipRepository { get; }

        void Save();
    }
}