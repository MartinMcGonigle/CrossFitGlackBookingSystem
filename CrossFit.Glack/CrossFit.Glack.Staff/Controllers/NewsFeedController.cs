using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Wrapper;
using CrossFit.Glack.Staff.ViewResult;
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

        public NewsFeedController(
            ILogger<NewsFeedController> logger,
            IRepositoryWrapper repository)
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

            if (model.UserGrantAcess.Any())
            {
                model.UserGrant = string.Join(",", model.UserGrantAcess);
            }

            _repository.NewsFeedRepository.Create(model);
            _repository.Save();

            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var data = _repository.NewsFeedRepository.FindByCondition(x => x.NewsFeedId == id).FirstOrDefault();

            if (data == null)
                return new NotFound();

            data.UserGrantAcess = data.UserGrant != null ? data.UserGrant.Split(',').ToList() : new List<string>();

            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(long id, NewsFeed model)
        {
            var data = _repository.NewsFeedRepository.FindByCondition(x => x.NewsFeedId == id).FirstOrDefault();

            if (data == null)
                return new NotFound();

            data.NewFeedMessage = model.NewFeedMessage;
            data.NewsFeedName = model.NewsFeedName;
            data.DateCreated = DateTime.Now;

            if (model.UserGrantAcess.Any())
            {
                data.UserGrant = string.Join(",", model.UserGrantAcess);
            }
            else
            {
                data.UserGrant = null;
            }

            _repository.NewsFeedRepository.Update(data);
            _repository.Save();

            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var data = _repository.NewsFeedRepository.FindByCondition(x => x.NewsFeedId == id).FirstOrDefault();

            if (data == null)
                return new NotFound();

            _repository.NewsFeedRepository.Delete(data);
            _repository.Save();

            return RedirectToAction(nameof(this.Index));
        }
    }
}