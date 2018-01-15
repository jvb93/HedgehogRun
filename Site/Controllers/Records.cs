using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.Entities;
using HedgehogRun.EntityFramework;
using HedgehogRun.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HedgehogRun.Controllers
{
    [Route("api/[controller]")]
    public class Records : Controller
    {
        private readonly ApplicationDbContext _context;

        public Records(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
           
            var maxTicks = _context.HogLogs.Max(x => x.Ticks);
            var totalTicks = _context.HogLogs.Sum(x => x.Ticks);
            var firstTick = _context.HogLogs.Min(x => x.PostTime);

            var topTenNights = _context.HogLogs.GroupBy(x => new {x.PostTime.Year, x.PostTime.Month, x.PostTime.Day})
                .OrderByDescending(x => x.Sum(y => y.Ticks)).Take(10).ToList()
                .Select(z => new {Date = z.First().PostTime.ToUnixTime(), Ticks = z.Sum(a => a.Ticks)});

            var topTenFastestPeriods = _context.HogLogs.OrderByDescending(x => x.Ticks).Take(10).ToList().Select(x=> new { Date = x.PostTime.ToUnixTime(), x.Ticks});
            

            JObject toReturn = new JObject();
            toReturn.Add("maxTicks", maxTicks);
            toReturn.Add("totalTicks", totalTicks);
            toReturn.Add("firstTick", firstTick.ToUnixTime());
            toReturn.Add("topTenFastestPeriods", JArray.FromObject(topTenFastestPeriods));
            toReturn.Add("topTenNights", JArray.FromObject(topTenNights));
            
            
            
            return Content(toReturn.ToString(), MimeTypeHelper.ApplicationJson);
        }      
    }
}
