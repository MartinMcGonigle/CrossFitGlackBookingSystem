using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Wrapper;
using CrossFit.Glack.Staff.ViewResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossFit.Glack.Staff.Controllers
{
    [Authorize(Roles = "SuperUser, Staff")]
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
            var membershipTypes = _repositoryWrapper.MembershipTypeRepository.FindAll();

            return View(membershipTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MembershipType model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _logger.LogInformation($"{prefix} - Creating membership type with title: {model.MembershipTypeTitle}");
            _repositoryWrapper.MembershipTypeRepository.Create(model);
            _repositoryWrapper.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int membershipTypeId)
        {
            var membershipType = _repositoryWrapper.MembershipTypeRepository.FindByCondition(x => x.MembershipTypeId == membershipTypeId).FirstOrDefault();
            if (membershipType == null)
                return new NotFound();

            return View(membershipType);
        }

        [HttpPost]
        public IActionResult Edit(int membershipTypeId, MembershipType model)
        {
            MembershipType data = _repositoryWrapper.MembershipTypeRepository.FindByCondition(x => x.MembershipTypeId == membershipTypeId).FirstOrDefault();

            if (data == null)
                return new NotFound();

            if (!ModelState.IsValid)
                return View(model);

            data.MembershipTypeTitle = model.MembershipTypeTitle;
            data.MembershipTypePrice = model.MembershipTypePrice;
            data.MembershipTypeDescription = model.MembershipTypeDescription;
            data.MembershipTypeActive = model.MembershipTypeActive;
            data.Type = model.Type;
            data.NumberOfClasses = model.NumberOfClasses;
            data.NumberOfMonths = model.NumberOfMonths;

            _logger.LogInformation($"{prefix} - Updating membership type with id: {data.MembershipTypeId}");
            _repositoryWrapper.MembershipTypeRepository.Update(data);
            _repositoryWrapper.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int membershipTypeId)
        {
            var membershipType = _repositoryWrapper.MembershipTypeRepository.FindByCondition(x => x.MembershipTypeId == membershipTypeId).FirstOrDefault();

            if (membershipType == null)
                return new NotFound();

            membershipType.MembershipTypeActive = false;

            _logger.LogInformation($"{prefix} - Deleting membership type with id: {membershipType.MembershipTypeId}");
            _repositoryWrapper.MembershipTypeRepository.Update(membershipType);
            _repositoryWrapper.Save();

            return RedirectToAction(nameof(this.Index));
        }
    }
}