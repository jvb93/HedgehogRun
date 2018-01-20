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
    public class CalculateRecordsTask : ICalculateRecordsTask
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IRecordService _recordService;

        public CalculateRecordsTask(ApplicationDbContext applicationDbContext, IRecordService recordService)
        {
            _applicationDbContext = applicationDbContext;
            _recordService = recordService;
        }

        public void Execute()
        {
            var record = _recordService.GetLastNightsRecords();
            if (record != null)
            {
                _applicationDbContext.Records.Add(record);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
