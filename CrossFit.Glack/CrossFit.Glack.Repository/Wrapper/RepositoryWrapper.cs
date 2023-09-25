using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Repository.Interfaces;
using CrossFit.Glack.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossFit.Glack.Repository.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationContext applicationContext;

        public RepositoryWrapper(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        private IMailServerRepository mailServerRepository;

        public IMailServerRepository MailServerRepository
        {
            get
            {
                if (mailServerRepository == null)
                {
                    mailServerRepository = new MailServerRepository(applicationContext);
                }

                return mailServerRepository;
            }
        }

        private IMembershipTypeRepository membershipTypeRepository;

        public IMembershipTypeRepository MembershipTypeRepository
        {
            get
            {
                if (membershipTypeRepository == null)
                {
                    membershipTypeRepository = new MembershipTypeRepository(applicationContext);
                }

                return membershipTypeRepository;
            }
        }

        private IMembershipRepository membershipRepository;

        public IMembershipRepository MembershipRepository
        {
            get
            {
                if (membershipRepository == null)
                {
                    membershipRepository = new MembershipRepository(applicationContext);
                }

                return membershipRepository;
            }
        }

        public void Save()
        {
            applicationContext.SaveChanges();
        }
    }
}