using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.Entities;
using HedgehogRun.EntityFramework;

namespace HedgehogRun.Hangfire
{
    public class CalculateRecordsTask : ICalculateRecordsTask
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CalculateRecordsTask(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Execute()
        {
            var now = DateTime.UtcNow;
            var fourteenHoursAgo = now.AddHours(-14);

            var logs = _applicationDbContext.HogLogs.Where(x => x.PostTime >= fourteenHoursAgo && x.PostTime < now);
            if (logs.Any())
            {
                Record newRecord = new Record();
                newRecord.EndDate = now;
                newRecord.StartDate = fourteenHoursAgo;
                newRecord.MaxTicks = logs.Max(x => x.Ticks);
                newRecord.FastestInterval = logs.OrderByDescending(x => x.Ticks).FirstOrDefault()?.PostTime ?? DateTime.MinValue;
                newRecord.TotalTicks = logs.Sum(x => x.Ticks);
                _applicationDbContext.Records.Add(newRecord);
                _applicationDbContext.SaveChanges();
            }       
        }
    }
}
