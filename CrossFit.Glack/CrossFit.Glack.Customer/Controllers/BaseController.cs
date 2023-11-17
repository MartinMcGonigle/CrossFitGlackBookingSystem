using CrossFit.Glack.Domain.OtherModels;
using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CrossFit.Glack.Customer.Controllers
{
    public class BaseController : Controller
    {
        internal readonly IRepositoryWrapper _repositoryWrapper;
        internal readonly IHttpContextAccessor _contextAccessor;
        internal string userId;

        public BaseController(
            IRepositoryWrapper repositoryWrapper,
            IHttpContextAccessor contextAccessor)
        {
            _repositoryWrapper = repositoryWrapper;
            _contextAccessor = contextAccessor;

            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
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

        [ViewData]
        public Step PreviousStep
        {
            get
            {
                var step = CurrentStep;

                if (step == null)
                {
                    return Steps.First.Value;
                }

                var currentNode = Steps.Find(step);
                if (currentNode != null && currentNode.Equals(Steps.First))
                {
                    return null;
                }

                return currentNode?.Previous?.Value;
            }
        }

        [HttpGet("Start")]
        public IActionResult Start()
        {
            var nextStep = NextStep;

            return RedirectToAction(nextStep.Name);
        }
    }
}