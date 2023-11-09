using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossFit.Glack.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class NewsFeedController : Controller
    {
        private readonly ILogger<NewsFeedController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public NewsFeedController(ILogger<NewsFeedController> logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        public IActionResult Index()
        {
            var data = _repositoryWrapper.NewsFeedRepository.FindAll().OrderByDescending(x => x.NewsFeedId).ToList();
            
            foreach (var item in data)
            {
                item.UserGrantAcess = item.UserGrant != null ? item.UserGrant.Split(',').ToList() : new List<string>();
            }

            return View(data);
        }
    }
}