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

        public HomeController(ITweetTask tweetTask, ICalculateRecordsTask calculateRecordsTask)
        {
            _tweetTask = tweetTask;
            _calculateRecordsTask = calculateRecordsTask;
        }

        public IActionResult Index()
        {
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
