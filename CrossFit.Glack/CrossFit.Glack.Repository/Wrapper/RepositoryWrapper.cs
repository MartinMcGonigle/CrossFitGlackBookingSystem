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

        private IStrengthRepository strengthRepository;

        public IStrengthRepository StrengthRepository
        {
            get
            {
                if (strengthRepository == null)
                {
                    strengthRepository = new StrengthRepository(applicationContext);
                }

                return strengthRepository;
            }
        }

        private IWarmupRepository warmupRepository;

        public IWarmupRepository WarmupRepository
        {
            get
            {
                if (warmupRepository == null)
                {
                    warmupRepository = new WarmupRepository(applicationContext);
                }

                return warmupRepository;
            }
        }

        private IWODRepository wodRepository;

        public IWODRepository WODRepository
        {
            get
            {
                if (wodRepository == null)
                {
                    wodRepository = new WODRepository(applicationContext);
                }

                return wodRepository;
            }
        }

        private IWorkoutRepository workoutRepository;

        public IWorkoutRepository WorkoutRepository
        {
            get
            {
                if (workoutRepository == null)
                {
                    workoutRepository = new WorkoutRepository(applicationContext);
                }

                return workoutRepository;
            }
        }

        private IClassRepository classRepository;

        public IClassRepository ClassRepository
        {
            get
            {
                if (classRepository == null)
                {
                    classRepository = new ClassRepository(applicationContext);
                }

                return classRepository;
            }
        }

        private IClassRegistrationRepository classRegistrationRepository;

        public IClassRegistrationRepository ClassRegistrationRepository
        {
            get
            {
                if (classRegistrationRepository == null)
                {
                    classRegistrationRepository = new ClassRegistrationRepository(applicationContext);
                }
                return classRegistrationRepository;
            }
        }

        public void Save()
        {
            applicationContext.SaveChanges();
        }
    }
}