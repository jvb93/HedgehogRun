using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.Entities;
using HedgehogRun.EntityFramework;
using HedgehogRun.Helpers;
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

        public HistoricalSpeed(ApplicationDbContext context)
        {
            _context = context;
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
          
            if (pastTwelveHourLogs.Any())
            {
                JArray tickData = new JArray();
                foreach (HogLog hogLog in pastTwelveHourLogs)
                {
                    JArray ticksDataPoint = new JArray();
                    ticksDataPoint.Add(hogLog.PostTime.ToUnixTime());
                    ticksDataPoint.Add(hogLog.Ticks);
                    tickData.Add(ticksDataPoint);               
                }
                ticks.Add("data", tickData);
            }
            else
            {
                ticks.Add("data", new JArray());
            }
            JObject tickToolTip = new JObject();
            tickToolTip.Add("valueSuffix", " MPH");
            ticks.Add("tooltip", tickToolTip);
            toReturn.Add("ticks", ticks);
            
            return Content(toReturn.ToString(), MimeTypeHelper.ApplicationJson);
        }      
    }
}
