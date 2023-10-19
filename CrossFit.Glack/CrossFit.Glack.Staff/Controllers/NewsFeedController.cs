using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CrossFit.Glack.Staff.Controllers
{
    [Authorize(Roles = "SuperUser, Staff")]
    public class NewsFeedController : Controller
    {
        private readonly ILogger<NewsFeedController> _logger;
        private readonly IRepositoryWrapper _repository;

        public NewsFeedController(ILogger<NewsFeedController> logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = _repository.NewsFeedRepository.FindAll().OrderByDescending(x => x.DateCreated);

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewsFeed model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.DateCreated = DateTime.Now;
            model.UserCreated = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _repository.NewsFeedRepository.Create(model);
            _repository.Save();

            return RedirectToAction(nameof(this.Index));
        }
    }
}