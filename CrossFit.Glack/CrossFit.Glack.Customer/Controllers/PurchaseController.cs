using CrossFit.Glack.Customer.ViewResult;
using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Domain.OtherModels;
using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossFit.Glack.Customer.Controllers
{
    [Route("PurchaseMembership")]
    [Authorize(Roles = "Customer")]
    public class PurchaseController : BaseController
    {
        private readonly ILogger<PurchaseController> _logger;
        public readonly string logPrefix = "Ctlr|Purchase";
        public PurchaseController(
            IRepositoryWrapper repositoryWrapper,
            IHttpContextAccessor contextAccessor,
            ILogger<PurchaseController> logger)
            : base (repositoryWrapper, contextAccessor)
        {
            Title = "Purchase Membership";

            Steps = new LinkedList<Step>();
            Steps.AddLast(new Step { Name = nameof(this.MembershipTypeSelection), Title = "Select Membership" });
            Steps.AddLast(new Step { Name = nameof(this.MembershipDetails), Title = "Membership Details" });
            Steps.AddLast(new Step { Name = nameof(this.Review), Title = "Review Details" });
            Steps.AddLast(new Step { Name = nameof(this.Finish), Title = "Purchase Complete", IncludeInReview = false });

            _logger = logger;
        }

        [HttpGet("MembershipTypeSelection")]
        public IActionResult MembershipTypeSelection()
        {
            var membership = _repositoryWrapper.MembershipRepository
                .FindByCondition(x => x.ApplicationUserId == userId)
                .FirstOrDefault() ?? new Membership();

            if (membership.MembershipActive)
            {
                _logger.LogInformation($"{logPrefix} - {userId} already has an active membership, do not allow them to purchase another until they cancel their current one");
                return RedirectToAction(nameof(ExistingMembership));
            }

            MembershipSelectionViewModel viewModel = new MembershipSelectionViewModel
            {
                Membership = membership,
                MembershipTypes = _repositoryWrapper.MembershipTypeRepository.FindAll().Where(x => x.MembershipTypeActive).ToList(),
            };

            ViewData["HeaderType"] = "Select Membership";

            return View(viewModel);
        }

        [HttpPost("MembershipTypeSelection")]
        public IActionResult MembershipTypeSelection(int selectedMembershipTypeId)
        {
            if (selectedMembershipTypeId == 0)
            {
                _logger.LogError($"{logPrefix} - {userId} is trying to purchase membership but selectedMembershipTypeId = {selectedMembershipTypeId}");
                return RedirectToAction(nameof(MembershipTypeSelection));
            }

            var membershipType = _repositoryWrapper.MembershipTypeRepository
                .FindByCondition(x => x.MembershipTypeId == selectedMembershipTypeId)
                .FirstOrDefault();

            if (membershipType == null)
            {
                _logger.LogError($"{logPrefix} - Could not find membership type with id: {selectedMembershipTypeId}");
                return new NotFound();
            }

            int month = 1;

            if (membershipType.Type == 2)
            {
                month = (int)membershipType.NumberOfMonths;
            }

            var membership = _repositoryWrapper.MembershipRepository
                .FindByCondition(x => x.ApplicationUserId == userId)
                .FirstOrDefault(); ;

            if (membership != null)
            {
                membership.MembershipActive = false;
                membership.MembershipCreationDate = DateTime.Now;
                membership.MembershipStartDate = DateTime.Now;
                membership.MembershipEndDate = DateTime.Now.AddMonths(month);
                membership.ApplicationUserId = userId;
                membership.MembershipTypeId = selectedMembershipTypeId;

                _logger.LogInformation($"{logPrefix} - Updating membership record {membership.MembershipId} for user with id: {userId}");
                _repositoryWrapper.MembershipRepository.Update(membership);
                _repositoryWrapper.Save();
            }
            else
            {
                Membership newMembership = new Membership
                {
                    MembershipActive = false,
                    MembershipCreationDate = DateTime.Now,
                    MembershipStartDate = DateTime.Now,
                    MembershipEndDate = DateTime.Now.AddMonths(month),
                    ApplicationUserId = userId,
                    MembershipTypeId = selectedMembershipTypeId,
                };

                _logger.LogInformation($"{logPrefix} - Creating membership record for user with id: {userId}");
                _repositoryWrapper.MembershipRepository.Create(newMembership);
                _repositoryWrapper.Save();
            }

            return RedirectToAction(NextStep.Name);
        }

        [HttpGet("MembershipDetails")]
        public IActionResult MembershipDetails()
        {
            var membership = _repositoryWrapper.MembershipRepository
                .FindByCondition(x => x.ApplicationUserId == userId)
                .FirstOrDefault();

            if (membership == null)
            {
                _logger.LogInformation($"{logPrefix} - Could not find membership belonging to user with id: {userId}");
                return new NotFound();
            }

            ViewData["HeaderType"] = "Membership Details";

            return View(membership);
        }

        [HttpPost("MembershipDetails")]
        public IActionResult MembershipDetails(bool selectedMembershipAutoRenew)
        {
            var membership = _repositoryWrapper.MembershipRepository
                .FindByCondition(x => x.ApplicationUserId == userId)
                .FirstOrDefault();

            if (membership == null)
            {
                _logger.LogInformation($"{logPrefix} - Could not find membership belonging to user with id: {userId}");
                return new NotFound();
            }

            membership.MembershipAutoRenew = selectedMembershipAutoRenew;

            _logger.LogInformation($"{logPrefix} - Updating membership: {membership.MembershipId} auto renew: {selectedMembershipAutoRenew}");
            _repositoryWrapper.MembershipRepository.Update(membership);
            _repositoryWrapper.Save();

            return RedirectToAction(NextStep.Name);
        }

        [HttpGet("Review")]
        public IActionResult Review()
        {
            var membership = _repositoryWrapper.MembershipRepository.GetUserMembership(userId);

            if (membership == null)
            {
                _logger.LogInformation($"{logPrefix}  - Could not find membership belonging to user with id: {userId}");
                return new NotFound();
            }

            ViewData["HeaderType"] = "Review Details";

            return View(membership);
        }

        [HttpGet("Finish")]
        public IActionResult Finish(long id)
        {
            var membership = _repositoryWrapper.MembershipRepository
                .FindByCondition(x => x.MembershipId == id)
                .FirstOrDefault();

            if (membership == null)
            {
                _logger.LogInformation($"{logPrefix} - Could not find membership with id: {id}");
                return new NotFound();
            }

            membership.MembershipActive = true;

            _logger.LogInformation($"{logPrefix} - Updating membership: {membership.MembershipId} : MembershipActive: {membership.MembershipActive}");
            _repositoryWrapper.MembershipRepository.Update(membership);
            _repositoryWrapper.Save();

            ViewData["HeaderType"] = "Purchase Complete";

            return View();
        }

        [HttpGet]
        public IActionResult ExistingMembership()
        {
            return View();
        }
    }
}