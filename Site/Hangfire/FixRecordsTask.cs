using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.Entities;
using HedgehogRun.EntityFramework;
using HedgehogRun.Services;
using Microsoft.Extensions.Configuration;

namespace HedgehogRun.Hangfire
{
    public class FixRecordsTask : IFixRecordsTask
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISolarCalculatorService _solarCalculatorService;
        private readonly IConfiguration _configuration;

        public FixRecordsTask(ApplicationDbContext applicationDbContext, ISolarCalculatorService solarCalculatorService, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _solarCalculatorService = solarCalculatorService;
            _configuration = configuration;
        }

        public void Execute()
        {
            var thisMorning = DateTime.UtcNow.Date.AddHours(15);
            for (var i = 0; i < 20; i++)
            {
                var enddate = thisMorning.AddDays(-i);
              
                var lastNight = _solarCalculatorService.GetSunsetTimeForLatLong(enddate,
                    TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"), decimal.Parse(_configuration["Latitude"]),
                    decimal.Parse(_configuration["Longitude"]));

                var logs = _applicationDbContext.HogLogs.Where(x => x.PostTime >= lastNight && x.PostTime < enddate);
                if (logs.Any())
                {
                    Record newRecord = new Record();
                    newRecord.EndDate = enddate;
                    newRecord.StartDate = lastNight;
                    newRecord.MaxTicks = logs.Max(x => x.Ticks);
                    newRecord.FastestInterval = logs.OrderByDescending(x => x.Ticks).FirstOrDefault()?.PostTime ?? DateTime.MinValue;
                    newRecord.TotalTicks = logs.Sum(x => x.Ticks);
                    _applicationDbContext.Records.Add(newRecord);
                    _applicationDbContext.SaveChanges();
                }

            }
        }
    }
}
