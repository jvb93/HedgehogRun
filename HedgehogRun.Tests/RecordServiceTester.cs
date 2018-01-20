using System;
using System.Collections.Generic;
using HedgehogRun.Entities;
using HedgehogRun.EntityFramework;
using HedgehogRun.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HedgehogRun.Tests
{
    [TestClass]
    public class RecordServiceTester
    {
        [TestMethod]
        public void CanGetRecords()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                context.HogLogs.Add(new HogLog()
                {
                    DidReconnect = false,
                    Humidity = 0,
                    Id = 1,
                    PostTime = DateTime.UtcNow.Date.AddHours(-1),
                    Ticks = 1000
                });
                context.SaveChanges();

                var service = new RecordService(new SolarCalculatorService(), context, config );
                var record = service.GetLastNightsRecords();

                Assert.IsNotNull(record);
            }
        }
    }

}
