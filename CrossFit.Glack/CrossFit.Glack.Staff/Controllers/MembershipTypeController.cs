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
        private readonly IRepositoryWrapper repositoryWrapper;
        private const string prefix = "CTRL|MembershipType";

        public MembershipTypeController(
            ILogger<MembershipTypeController> logger,
            IRepositoryWrapper repositoryWrapper)
        {
            this._logger = logger;
            this.repositoryWrapper = repositoryWrapper;
        }

        public IActionResult Index()
        {
            this._logger.LogInformation($"{prefix} - Displaying all membership types");
            var membershipTypes = this.repositoryWrapper.MembershipTypeRepository.FindAll();

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

            this._logger.LogInformation($"{prefix} - Creating membership type with name: {model.MembershipTypeTitle}");
            this.repositoryWrapper.MembershipTypeRepository.Create(model);
            this.repositoryWrapper.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int membershipTypeId)
        {
            var membershipType = this.repositoryWrapper.MembershipTypeRepository.FindByCondition(x => x.MembershipTypeId == membershipTypeId).FirstOrDefault();
            if (membershipType == null)
                return new NotFound();

            return this.View(membershipType);
        }

        [HttpPost]
        public IActionResult Edit(int membershipTypeId, MembershipType model)
        {
            MembershipType data = this.repositoryWrapper.MembershipTypeRepository.FindByCondition(x => x.MembershipTypeId == membershipTypeId).FirstOrDefault();

            if (data == null)
                return new NotFound();

            if (!ModelState.IsValid)
                return this.View(model);

            data.MembershipTypeTitle = model.MembershipTypeTitle;
            data.MembershipTypePrice = model.MembershipTypePrice;
            data.MembershipTypeDescription = model.MembershipTypeDescription;

            this._logger.LogInformation($"{prefix} - Updating membership type with id: {model.MembershipTypeId}");
            this.repositoryWrapper.MembershipTypeRepository.Update(data);
            this.repositoryWrapper.Save();

            return RedirectToAction(nameof(this.Index));
        }
    }
}