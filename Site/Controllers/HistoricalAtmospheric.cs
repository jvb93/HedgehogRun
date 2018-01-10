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
    public class HistoricalAtmospheric : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoricalAtmospheric(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            var twelveHoursAgoUTC = DateTime.UtcNow.AddHours(-12);
            var pastTwelveHourAtmospheric = _context.HogLogs.Where(x=>x.PostTime >= twelveHoursAgoUTC);


            JObject toReturn = new JObject();

            JObject temperature = new JObject();
            temperature.Add("name", "Temperature");
           

            JObject humidity = new JObject();
            humidity.Add("name", "Humidity");


            if (pastTwelveHourAtmospheric.Any())
            {
                JArray temperatureData = new JArray();
                JArray humidityData = new JArray();
                foreach (HogLog hogLog in pastTwelveHourAtmospheric)
                {
                    JArray temperatureDataPoint = new JArray();
                    temperatureDataPoint.Add(hogLog.PostTime.ToUnixTime());
                    temperatureDataPoint.Add(hogLog.TemperatureF);
                    temperatureData.Add(temperatureDataPoint);

                    JArray humidityDataPoint = new JArray();
                    humidityDataPoint.Add(hogLog.PostTime.ToUnixTime());
                    humidityDataPoint.Add(hogLog.Humidity);
                    humidityData.Add(humidityDataPoint);
                }

                humidity.Add("data", humidityData);
                temperature.Add("data", temperatureData);
            }
            else
            {
                humidity.Add("data", new JArray());
                temperature.Add("data", new JArray());
            }
            JObject temperatureTooltip = new JObject();
            temperatureTooltip.Add("valueSuffix", " °F");
            temperature.Add("tooltip", temperatureTooltip);


            JObject humidityTooltip = new JObject();
            humidityTooltip.Add("valueSuffix", "%");
            humidity.Add("tooltip", humidityTooltip);

            toReturn.Add("temperature", temperature);
            toReturn.Add("humidity", humidity);
        
            return Content(toReturn.ToString(), MimeTypeHelper.ApplicationJson);
        }

       

        
    }
}
