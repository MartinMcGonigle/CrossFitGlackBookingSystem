﻿using CrossFit.Glack.Repository.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CrossFit.Glack.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class WorkoutController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public WorkoutController(IRepositoryWrapper repositoryWrapper)
        {
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
    }
}