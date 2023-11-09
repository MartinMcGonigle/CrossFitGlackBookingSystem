using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private string? userId;

        public ClassController(
            ILogger<ClassController> logger,
            IRepositoryWrapper repositoryWrapper,
            IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;

            if (contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                userId = contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
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

            _logger.LogInformation($"{logPrefix} - Displaying classes, DateTime: {dateTime}");

            IEnumerable<Class> data;
            if (dateTime.Date == DateTime.Today)
            {
                DateTime currentDateTime = DateTime.Now;
                dateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, currentDateTime.Minute, 0);
                data = _repositoryWrapper.ClassRepository.GetTodaysClassesCustomer(dateTime, userId);
            }
            else if (dateTime.Date > DateTime.Today)
            {
                data = _repositoryWrapper.ClassRepository.GetClassesInFutureCustomer(dateTime, userId);
            }
            else
            {
                data = Enumerable.Empty<Class>();
            }

            ViewData["Date"] = dateTime;

            return View(data);
        }

        [HttpPost]
        public IActionResult ReserveClass(long classId)
        {
            try
            {
                var classRegistration = new ClassRegistration
                {
                    ClassId = classId,
                    UserId = userId,
                };

                _repositoryWrapper.ClassRegistrationRepository.Create(classRegistration);
                _repositoryWrapper.Save();

                _logger.LogInformation($"{logPrefix} - {userId} made reservation for class: {classId}");
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                _logger.LogError($"{logPrefix} - Error trying to reserve a spot in class: {classId} for user: {userId}. {ex.Message}");
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult WhosComing(long id, string date)
        {
            DateTime dateTime;

            if (string.IsNullOrEmpty(date) || !DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                dateTime = DateTime.Now;
            }

            var classRegistration = _repositoryWrapper.ClassRegistrationRepository.GetReservations(id);

            ViewData["Date"] = dateTime;
            return View(classRegistration);
        }

        [HttpPost]
        public IActionResult DeleteClassReservation(long reservationId)
        {
            try
            {
                var classRegistration = _repositoryWrapper.ClassRegistrationRepository.FindByCondition(x => x.ClassRegistrationId == reservationId).FirstOrDefault();

                if (classRegistration == null)
                {
                    _logger.LogError($"{logPrefix} - {userId} tried to cancel class registration: {reservationId} but it could not be found");
                    return Json(new { success = false });
                }

                _repositoryWrapper.ClassRegistrationRepository.Delete(classRegistration);
                _repositoryWrapper.Save();

                _logger.LogInformation($"{logPrefix} - {userId} canceled class registration: {reservationId}");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{logPrefix} - Error trying to cancel {userId} reservation, ClassRegistrationId: {reservationId}.  {ex.Message}");
                return Json(new { success = false });
            }
        }
    }
}
