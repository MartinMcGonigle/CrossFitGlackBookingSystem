using CrossFit.Glack.Domain.Models;
using CrossFit.Glack.Domain.OtherModels;
using CrossFit.Glack.Repository.Wrapper;
using CrossFit.Glack.Staff.ViewResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CrossFit.Glack.Staff.Controllers
{
    [Authorize(Roles = "SuperUser, Staff")]
    public class WorkoutController : Controller
    {
        private readonly ILogger<WorkoutController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public readonly string logPrefix = "Ctlr|Workout";

        public WorkoutController(ILogger<WorkoutController> logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult Index(string date, int offset = 0)
        {
            DateTime dateTime;
            if (string.IsNullOrEmpty(date))
            {
                dateTime = DateTime.Today;
            }
            else
            {
                if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    dateTime = DateTime.Today;
                }
            }

            // Adjust the date based on the offset
            dateTime = dateTime.AddDays(offset);

            var workout = _repositoryWrapper.WorkoutRepository.GetWorkoutByDate(dateTime);

            ViewData["Date"] = dateTime;

            return this.View(workout);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(WorkoutViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.WarmupDescription) &&
                string.IsNullOrWhiteSpace(model.StrengthDescription) &&
                string.IsNullOrWhiteSpace(model.WODDescription))
            {
                ModelState.AddModelError("", "At least one section (Warmup, Strength, or WOD) is required.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingWorkout = _repositoryWrapper.WorkoutRepository.FindByCondition(x => x.Date == model.Date && x.Active).FirstOrDefault();

            if (existingWorkout != null)
            {
                ModelState.AddModelError("Date", $"There is already a workout scheduled for {model.Date.ToShortDateString()}");
                return View(model);
            }

            var workout = new Workout
            {
                Date = model.Date,
                Active = model.Active,
            };

            if (!string.IsNullOrWhiteSpace(model.WarmupDescription))
            {
                workout.WarmupId = CreateWarmup(model.WarmupDescription);
            }

            if (!string.IsNullOrWhiteSpace(model.StrengthDescription))
            {
                workout.StrengthId = CreateStrength(model.StrengthDescription);
            }

            if (!string.IsNullOrWhiteSpace(model.WODDescription))
            {
                workout.WODId = CreateWOD(model.WODDescription);
            }

            _logger.LogInformation($"Creating workout for: {model.Date.ToShortDateString()}");

            // Create the Workout
            _repositoryWrapper.WorkoutRepository.Create(workout);
            _repositoryWrapper.Save();

            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var workout = _repositoryWrapper.WorkoutRepository.GetWorkoutById(id);

            if (workout == null)
                return new NotFound();

            WorkoutViewModel workoutViewModel = new WorkoutViewModel
            {
                WorkoutId = workout.WorkoutId,
                Date = workout.Date,
                WarmupDescription = workout.Warmup?.Description,
                StrengthDescription = workout.Strength?.Description,
                WODDescription = workout.WOD?.Description,
                Active = workout.Active,
            };

            return View(workoutViewModel);
        }

        [HttpPost]
        public IActionResult Edit(long workoutId, WorkoutViewModel model)
        {
            var workout = _repositoryWrapper.WorkoutRepository.GetWorkoutById(workoutId);

            if (workout == null)
                return new NotFound();
            
            if (string.IsNullOrWhiteSpace(model.WarmupDescription) &&
                string.IsNullOrWhiteSpace(model.StrengthDescription) &&
                string.IsNullOrWhiteSpace(model.WODDescription))
            {
                ModelState.AddModelError("", "At least one section (Warmup, Strength, or WOD) is required.");
                return View(model);
            }

            if (!ModelState.IsValid)
                return View(model);

            if (!string.IsNullOrWhiteSpace(model.WarmupDescription))
            {
                if (workout.WarmupId == null)
                {
                    workout.WarmupId = CreateWarmup(model.WarmupDescription);
                }
                else
                {
                    workout.Warmup.Description = model.WarmupDescription;
                }         
            }
            else
            {
                workout.WarmupId = null;
            }

            if (!string.IsNullOrWhiteSpace(model.StrengthDescription))
            {
                if (workout.StrengthId == null)
                {
                    workout.StrengthId = CreateStrength(model.StrengthDescription);
                }
                else
                {
                    workout.Strength.Description = model.StrengthDescription;
                }
            }
            else
            {
                workout.StrengthId = null;
            }

            if (!string.IsNullOrWhiteSpace(model.WODDescription))
            {
                if (workout.WODId == null)
                {
                    workout.WODId = CreateWOD(model.WODDescription);
                }
                else
                {
                    workout.WOD.Description = model.WODDescription;
                }
            }
            else
            {
                workout.WODId = null;
            }

            _logger.LogInformation($"Updating workout with id: {workout.WorkoutId}");

            // Update the Workout
            _repositoryWrapper.WorkoutRepository.Update(workout);
            _repositoryWrapper.Save();

            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var workout = _repositoryWrapper.WorkoutRepository.FindByCondition(x => x.WorkoutId == id).FirstOrDefault();
            
            if (workout == null)
                return new NotFound();

            workout.Active = false;

            _logger.LogInformation($"Deleting workout with id: {workout.WorkoutId}");

            _repositoryWrapper.WorkoutRepository.Update(workout);
            _repositoryWrapper.Save();

            return RedirectToAction(nameof(this.Index));
        }

        private long CreateWarmup(string description)
        {
            var warmup = new Warmup
            {
                Description = description,
                Order = 1,
            };

            // Create Warmup
            _repositoryWrapper.WarmupRepository.Create(warmup);
            _repositoryWrapper.Save();

            return warmup.WarmupId;
        }

        private long CreateStrength(string description)
        {
            var strength = new Strength
            {
                Description = description,
                Order = 2,
            };

            // Create Strength
            _repositoryWrapper.StrengthRepository.Create(strength);
            _repositoryWrapper.Save();

            return strength.StrengthId;
        }

        private long CreateWOD(string description)
        {
            var wod = new WOD
            {
                Description = description,
                Order = 3,
            };

            // Create WOD
            _repositoryWrapper.WODRepository.Create(wod);
            _repositoryWrapper.Save();

            return wod.WODId;
        }
    }
}