using CrossFit.Glack.Domain.Context;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Interfaces;

namespace CrossFit.Glack.Repository.Repository
{
    public class NewsFeedRepository : RepositoryBase<NewsFeed>, INewsFeedRepository
    {
        public NewsFeedRepository(ApplicationContext context) : base(context)
        {
            
        }
    }
}