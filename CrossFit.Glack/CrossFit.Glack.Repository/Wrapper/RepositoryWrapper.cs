using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Repository.Interfaces;
using CrossFit.Glack.Repository.Repository;

namespace CrossFit.Glack.Repository.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationContext applicationContext;

        public RepositoryWrapper(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        private IMailServerRepository _mailServerRepository;

        public IMailServerRepository MailServerRepository
        {
            get
            {
                if (_mailServerRepository == null)
                {
                    _mailServerRepository = new MailServerRepository(applicationContext);
                }

                return _mailServerRepository;
            }
        }

        private IMembershipTypeRepository _membershipTypeRepository;

        public IMembershipTypeRepository MembershipTypeRepository
        {
            get
            {
                if (_membershipTypeRepository == null)
                {
                    _membershipTypeRepository = new MembershipTypeRepository(applicationContext);
                }

                return _membershipTypeRepository;
            }
        }

        private IMembershipRepository _membershipRepository;

        public IMembershipRepository MembershipRepository
        {
            get
            {
                if (_membershipRepository == null)
                {
                    _membershipRepository = new MembershipRepository(applicationContext);
                }

                return _membershipRepository;
            }
        }

        private IStrengthRepository _strengthRepository;

        public IStrengthRepository StrengthRepository
        {
            get
            {
                if (_strengthRepository == null)
                {
                    _strengthRepository = new StrengthRepository(applicationContext);
                }

                return _strengthRepository;
            }
        }

        private IWarmupRepository _warmupRepository;

        public IWarmupRepository WarmupRepository
        {
            get
            {
                if (_warmupRepository == null)
                {
                    _warmupRepository = new WarmupRepository(applicationContext);
                }

                return _warmupRepository;
            }
        }

        private IWODRepository _wodRepository;

        public IWODRepository WODRepository
        {
            get
            {
                if (_wodRepository == null)
                {
                    _wodRepository = new WODRepository(applicationContext);
                }

                return _wodRepository;
            }
        }

        private IWorkoutRepository _workoutRepository;

        public IWorkoutRepository WorkoutRepository
        {
            get
            {
                if (_workoutRepository == null)
                {
                    _workoutRepository = new WorkoutRepository(applicationContext);
                }

                return _workoutRepository;
            }
        }

        private IClassRepository _classRepository;

        public IClassRepository ClassRepository
        {
            get
            {
                if (_classRepository == null)
                {
                    _classRepository = new ClassRepository(applicationContext);
                }

                return _classRepository;
            }
        }

        private IClassRegistrationRepository _classRegistrationRepository;

        public IClassRegistrationRepository ClassRegistrationRepository
        {
            get
            {
                if (_classRegistrationRepository == null)
                {
                    _classRegistrationRepository = new ClassRegistrationRepository(applicationContext);
                }
                return _classRegistrationRepository;
            }
        }

        private INewsFeedRepository _newsFeedRepository;

        public INewsFeedRepository NewsFeedRepository
        {
            get
            {
                if (_newsFeedRepository == null)
                {
                    _newsFeedRepository = new NewsFeedRepository(applicationContext);
                }

                return _newsFeedRepository;
            }
        }

        public void Save()
        {
            applicationContext.SaveChanges();
        }
    }
}