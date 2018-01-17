using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.Entities;
using HedgehogRun.EntityFramework;
using HedgehogRun.Helpers;
using HedgehogRun.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HedgehogRun.Controllers
{
    [Route("api/[controller]")]
    public class HistoricalSpeed : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConvertService _convertService;

        public HistoricalSpeed(ApplicationDbContext context, IConvertService convertService)
        {
            _context = context;
            _convertService = convertService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            var twelveHoursAgoUTC = DateTime.UtcNow.AddHours(-12);
            var pastTwelveHourLogs = _context.HogLogs.Where(x=>x.PostTime >= twelveHoursAgoUTC);


            JObject toReturn = new JObject();

            JObject ticks = new JObject();
            ticks.Add("name", "Speed");

            JObject distance = new JObject();
            distance.Add("name", "Distance");

            if (pastTwelveHourLogs.Any())
            {
                JArray tickData = new JArray();
                JArray distanceData = new JArray();
                int totalTicks = 0;
                foreach (HogLog hogLog in pastTwelveHourLogs)
                {
                    JArray ticksDataPoint = new JArray();
                    ticksDataPoint.Add(hogLog.PostTime.ToUnixTime());
                    ticksDataPoint.Add(_convertService.ConvertTicksToMilesPerHour(hogLog.Ticks));
                    tickData.Add(ticksDataPoint);

                    JArray distanceDataPoint = new JArray();
                    distanceDataPoint.Add(hogLog.PostTime.ToUnixTime());
                    totalTicks += hogLog.Ticks;
                    distanceDataPoint.Add(_convertService.ConvertTicksToMiles(totalTicks));
                    distanceData.Add(distanceDataPoint);

                }
                ticks.Add("data", tickData);
                distance.Add("data", distanceData);
            }
            else
            {
                ticks.Add("data", new JArray());
                distance.Add("data", new JArray());
            }
            JObject tickToolTip = new JObject();
            tickToolTip.Add("valueSuffix", " MPH");
            ticks.Add("tooltip", tickToolTip);
            ticks.Add("type", "area");


            JObject distanceToolTip = new JObject();
            distanceToolTip.Add("valueSuffix", " Miles");
            distance.Add("tooltip", distanceToolTip);
            distance.Add("yAxis", 1);
            distance.Add("type", "line");
            distance.Add("color", "#FFF");


            toReturn.Add("ticks", ticks);
            toReturn.Add("distance", distance);
            
            return Content(toReturn.ToString(), MimeTypeHelper.ApplicationJson);
        }      
    }
}
