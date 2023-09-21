using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Repository
{
    public class MailServerRepository : RepositoryBase<MailServer>, IMailServerRepository
    {
        public MailServerRepository(ApplicationContext context) : base(context)
        {
        }
    }
}