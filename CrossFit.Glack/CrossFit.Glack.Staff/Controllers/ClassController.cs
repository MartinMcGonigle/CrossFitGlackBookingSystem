using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossFit.Glack.Staff.Controllers
{
    [Authorize(Roles = "SuperUser, Staff")]
    public class ClassController : Controller
    {
        private readonly ILogger<ClassController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public readonly string logPrefix = "Ctlr|Class";

        public ClassController(
            ILogger<ClassController> logger,
            IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}