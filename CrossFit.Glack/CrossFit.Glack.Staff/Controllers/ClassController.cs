using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Repository.Wrapper;
using CrossFit.Glack.Service.Users;
using CrossFit.Glack.Staff.ViewResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace CrossFit.Glack.Staff.Controllers
{
    [Authorize(Roles = "SuperUser, Staff")]
    public class ClassController : Controller
    {
        private readonly ILogger<ClassController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly UserManagerService _userManagerService;
        public readonly string logPrefix = "Ctlr|Class";

        public ClassController(
            ILogger<ClassController> logger,
            IRepositoryWrapper repositoryWrapper,
            UserManagerService userManagerService)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
            _userManagerService = userManagerService;
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
        public IActionResult Create()
        {
            ViewBag.Coaches = _userManagerService.GetCoachesSelectList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Class model)
        {
            ViewBag.Coaches = _userManagerService.GetCoachesSelectList();

            if (model.Date <= DateTime.Now)
            {
                ModelState.AddModelError("Date", "You can not schedule a class for a date and time in the past.");
                return View(model);
            }

            if (string.IsNullOrEmpty(model.InstructorId))
            {
                ModelState.AddModelError("InstructorId", "Please select which coach that will be leading the class.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingClasses = _repositoryWrapper.ClassRepository.FindAll();
            model.DateTimeEnd = model.Date.AddMinutes(model.DurationInMinutes);

            if (existingClasses.Any(x => x.Date == model.Date || x.Date.AddMinutes(x.DurationInMinutes) == model.DateTimeEnd || (model.Date > x.Date && model.Date < x.Date.AddMinutes(x.DurationInMinutes)) || (model.DateTimeEnd > x.Date && model.DateTimeEnd < x.Date.AddMinutes(x.DurationInMinutes))))
            {
                ModelState.AddModelError("Date", "Classes cannot overlap.");
                return View(model);
            }

            _repositoryWrapper.ClassRepository.Create(model);
            _repositoryWrapper.Save();
            
            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _repositoryWrapper.ClassRepository.FindByCondition(x => x.ClassId == id).FirstOrDefault();

            if (model == null)
                return new NotFound();

            ViewBag.Coaches = _userManagerService.GetCoachesSelectList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(long classId, Class model)
        {
            var existingClass = _repositoryWrapper.ClassRepository.FindByCondition(x => x.ClassId == classId).FirstOrDefault();

            if (existingClass == null)
                return new NotFound();

            ViewBag.Coaches = _userManagerService.GetCoachesSelectList();

            if (model.Date <= DateTime.Now)
            {
                ModelState.AddModelError("Date", "You can not schedule a class for a date and time in the past.");
                return View(model);
            }

            if (string.IsNullOrEmpty(model.InstructorId))
            {
                ModelState.AddModelError("InstructorId", "Please select which coach that will be leading the class.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var existingClasses = _repositoryWrapper.ClassRepository.FindAll().Where(c => c.ClassId != model.ClassId);
            model.DateTimeEnd = model.Date.AddMinutes(model.DurationInMinutes);

            if (existingClasses.Any(x => x.Date == model.Date || x.Date.AddMinutes(x.DurationInMinutes) == model.DateTimeEnd || (model.Date > x.Date && model.Date < x.Date.AddMinutes(x.DurationInMinutes)) || (model.DateTimeEnd > x.Date && model.DateTimeEnd < x.Date.AddMinutes(x.DurationInMinutes))))
            {
                ModelState.AddModelError("Date", "Classes cannot overlap.");
                return View(model);
            }

            // Update the existing class properties
            existingClass.Title = model.Title;
            existingClass.Date = model.Date;
            existingClass.DurationInMinutes = model.DurationInMinutes;
            existingClass.MaxAttendees = model.MaxAttendees;
            existingClass.AvailableSpots = model.AvailableSpots;
            existingClass.InstructorId = model.InstructorId;
            existingClass.DateTimeEnd = model.DateTimeEnd;

            _repositoryWrapper.ClassRepository.Update(existingClass);
            _repositoryWrapper.Save();

            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var model = _repositoryWrapper.ClassRepository.FindByCondition(x => x.ClassId == id).FirstOrDefault();

            if (model == null)
                return new NotFound();

            model.Active = false;

            _repositoryWrapper.ClassRepository.Update(model);
            _repositoryWrapper.Save();

            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult WhosComing(long id, string dateTime)
        {
            var classRegistration = _repositoryWrapper.ClassRegistrationRepository.GetReservations(id);

            return View(classRegistration);
        }

        [HttpPost]
        public IActionResult MarkUserPresent(long classRegistrationId, long classId, string userId)
        {
            var classRegistration = _repositoryWrapper.ClassRegistrationRepository.FindByCondition(x => x.ClassRegistrationId == classRegistrationId && x.ClassId == classId && x.UserId == userId).FirstOrDefault();

            if (classRegistration != null)
            {
                classRegistration.Present = true;

                _repositoryWrapper.ClassRegistrationRepository.Update(classRegistration);
                _repositoryWrapper.Save();

                return Ok("User marked as present.");
            }
            else
            {
                return NotFound("Class registration not found.");
            }
        }

        [HttpPost]
        public IActionResult MarkUserAbsent(long classRegistrationId, long classId, string userId)
        {
            var classRegistration = _repositoryWrapper.ClassRegistrationRepository.FindByCondition(x => x.ClassRegistrationId == classRegistrationId && x.ClassId == classId && x.UserId == userId).FirstOrDefault();

            if (classRegistration != null)
            {
                classRegistration.Present = false;

                _repositoryWrapper.ClassRegistrationRepository.Update(classRegistration);
                _repositoryWrapper.Save();

                return Ok("User marked as absent.");
            }
            else
            {
                return NotFound("Class registration not found.");
            }
        }
    }
}