using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossFit.Glack.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class MembershipTypeController : Controller
    {
        private readonly ILogger<MembershipTypeController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private const string prefix = "CTRL|MembershipType";

        public MembershipTypeController(
            ILogger<MembershipTypeController> logger,
            IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation($"{prefix} - Displaying all membership types");
            var membershipTypes = _repositoryWrapper.MembershipTypeRepository.FindAll().Where(x => x.MembershipTypeActive);

            return View(membershipTypes);
        }
    }
}