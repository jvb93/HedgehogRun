using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.Entities;
using HedgehogRun.EntityFramework;

namespace HedgehogRun.Hangfire
{
    public class FixRecordsTask : IFixRecordsTask
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FixRecordsTask(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Execute()
        {
            var thisMorning = DateTime.UtcNow.Date.AddHours(15);
            for (var i = 0; i < 20; i++)
            {
                var enddate = thisMorning.AddDays(-i);
                var startdate = enddate.AddHours(-14);

                var logs = _applicationDbContext.HogLogs.Where(x => x.PostTime >= startdate && x.PostTime < enddate);
                if (logs.Any())
                {
                    Record newRecord = new Record();
                    newRecord.EndDate = enddate;
                    newRecord.StartDate = startdate;
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
