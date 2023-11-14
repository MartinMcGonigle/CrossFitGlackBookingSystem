using CrossFit.Glack.Domain.OtherModels;
using CrossFit.Glack.Repository.Wrapper;
using CrossFit.Glack.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossFit.Glack.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class PurchaseController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public PurchaseController(IRepositoryWrapper repositoryWrapper)
        {
            Title = "Purchase Membership";

            Steps = new LinkedList<Step>();
            Steps.AddLast(new Step { Name = nameof(this.MembershipTypeSelection), Title = "Select Membership" });
            Steps.AddLast(new Step { Name = nameof(this.Review), Title = "Review Details" });
            Steps.AddLast(new Step { Name = nameof(this.Finish), Title = "Complete Purchase", IncludeInReview = false });

            _repositoryWrapper = repositoryWrapper;
        }

        [ViewData]
        public string Title { get; set; }

        [ViewData]
        public LinkedList<Step> Steps { get; set; }

        [ViewData]
        public Step CurrentStep
        {
            get
            {
                return Steps.SingleOrDefault(x => x.Name == ControllerContext.RouteData.Values["action"].ToString());
            }
        }

        [ViewData]
        public Step NextStep
        {
            get
            {
                var step = CurrentStep;

                if (step == null)
                {
                    return Steps.First.Value;
                }

                var currentNode = Steps.Find(step);

                return currentNode?.Next?.Value;
            }
        }

        [ViewData]
        public int CurrentStepNumber
        {
            get
            {
                if (CurrentStep == null)
                {
                    return 0;
                }

                var counter = 0;

                foreach (var step in Steps)
                {
                    counter++;
                    if (CurrentStep.Equals(step))
                    {
                        break;
                    }
                }

                return counter;
            }
        }

        [HttpGet("Start")]
        public IActionResult Start()
        {
            var nextStep = NextStep;

            return RedirectToAction(nextStep.Name);
        }

        [HttpGet]
        public IActionResult MembershipTypeSelection()
        {
            var membershipTypes = _repositoryWrapper.MembershipTypeRepository.FindAll().Where(x => x.MembershipTypeActive).ToList();
            ViewData["HeaderType"] = "Select Membership";
            return View(membershipTypes);
        }

        [HttpGet]
        public IActionResult Review()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Finish()
        {
            return View();
        }
    }
}