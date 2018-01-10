using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.EntityFramework;
using HedgehogRun.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HedgehogRun.Controllers
{
    [Route("api/[controller]")]
    public class CurrentAtmospheric : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrentAtmospheric(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            var current = _context.HogLogs.OrderByDescending(x => x.PostTime).Take(1).FirstOrDefault();

            JObject toReturn = new JObject();
            if (current != null)
            {
                toReturn.Add("temperature", current.TemperatureF);
                toReturn.Add("humidity", current.Humidity);
                toReturn.Add("lastUpdated", current.PostTime.ToUnixTime());
            }
            else
            {
                toReturn.Add("temperature", 0);
                toReturn.Add("humidity", 0);
                toReturn.Add("lastUpdated", "never");
            }

            return Content(toReturn.ToString(), MimeTypeHelper.ApplicationJson);
        }

       

        
    }
}
