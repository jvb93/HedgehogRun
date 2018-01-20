using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.Entities;
using HedgehogRun.EntityFramework;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Microsoft.Extensions.Configuration;

namespace HedgehogRun.Services
{
    public class RecordService :IRecordService
    {
        private readonly ISolarCalculatorService _solarCalculatorService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;

        public RecordService(ISolarCalculatorService solarCalculatorService, ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _solarCalculatorService = solarCalculatorService;
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
        }

        public Record GetLastNightsRecords()
        {
             
            var now = DateTime.UtcNow;
            var yesterday = DateTime.UtcNow.Date.AddDays(-1);

            var timeZone = TimeZoneInfo.Utc;//FindSystemTimeZoneById("Pacific Standard Time");
            var lastNightSunset = _solarCalculatorService.GetSunsetTimeForLatLong(
                yesterday, timeZone,
                decimal.Parse(_configuration["Latitude"]), decimal.Parse(_configuration["Longitude"])).AddHours(-8);


            var logs = _applicationDbContext.HogLogs.Where(x => x.PostTime >= lastNightSunset && x.PostTime < now);
            if (logs.Any())
            {
                Record newRecord = new Record();
                newRecord.EndDate = now;
                newRecord.StartDate = lastNightSunset;
                newRecord.MaxTicks = logs.Max(x => x.Ticks);
                newRecord.FastestInterval = logs.OrderByDescending(x => x.Ticks).FirstOrDefault()?.PostTime ?? DateTime.MinValue;
                newRecord.TotalTicks = logs.Sum(x => x.Ticks);
                return newRecord;
               
            }

            return null;
        }
    }
}
