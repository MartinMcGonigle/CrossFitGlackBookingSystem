using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace CrossFit.Glack.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ClassController : Controller
    {
        private readonly ILogger<ClassController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public readonly string logPrefix = "Ctlr|Class";

        public ClassController(ILogger<ClassController> logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult Index(string date, int offset = 0)
        {
            DateTime dateTime;

            if (string.IsNullOrEmpty(date) || !DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                dateTime = DateTime.Now;
            }

            // Adjust the date based on the offset
            dateTime = dateTime.AddDays(offset);

            IEnumerable<Class> data;
            if (dateTime.Date == DateTime.Today)
            {
                DateTime currentDateTime = DateTime.Now;
                dateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, currentDateTime.Minute, 0);
                data = _repositoryWrapper.ClassRepository.GetTodaysClasses(dateTime);
            }
            else if (dateTime.Date > DateTime.Today)
            {
                data = _repositoryWrapper.ClassRepository.GetClassesInFuture(dateTime);
            }
            else
            {
                data = Enumerable.Empty<Class>();
            }

            ViewData["Date"] = dateTime;

            return View(data);
        }

        [HttpGet]
        public IActionResult ReserveClass(long id, string date)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var classRegistration = new ClassRegistration
            {
                ClassId = id,
                UserId = userId,
            };

            _repositoryWrapper.ClassRegistrationRepository.Create(classRegistration);
            _repositoryWrapper.Save();

            return RedirectToAction("Index", new { date = date});
        }

        [HttpGet]
        public IActionResult WhosComing(long id, string date)
        {
            var classRegistration = _repositoryWrapper.ClassRegistrationRepository.GetReservations(id);

            return View(classRegistration);
        }
    }
}
