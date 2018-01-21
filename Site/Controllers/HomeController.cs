using System;
using Hangfire;
using HedgehogRun.Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HedgehogRun.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICalculateRecordsTask _calculateRecordsTask;
        private readonly ITweetTask _tweetTask;
        private readonly IFixRecordsTask _fixRecordsTask;

        public HomeController(ITweetTask tweetTask, ICalculateRecordsTask calculateRecordsTask, IFixRecordsTask fixRecordsTask)
        {
            _tweetTask = tweetTask;
            _calculateRecordsTask = calculateRecordsTask;
            _fixRecordsTask = fixRecordsTask;
        }

        public IActionResult Index()
        {
            //BackgroundJob.Enqueue(() => _fixRecordsTask.Execute());
            RecurringJob.AddOrUpdate(() => _calculateRecordsTask.Execute(), Cron.Daily(8), TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
            RecurringJob.AddOrUpdate(() => _tweetTask.Execute(), Cron.Daily(9), TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
