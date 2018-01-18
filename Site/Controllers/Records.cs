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
           
            var maxTicks = _context.Records.Max(x => x.MaxTicks);
            var totalTicks = _context.Records.Sum(x => x.TotalTicks);
            var firstTick = _context.HogLogs.Min(x => x.PostTime);

            var topTenNights = _context.Records.GroupBy(x => new {x.StartDate.Year, x.StartDate.Month, x.StartDate.Day})
                .OrderByDescending(x => x.Sum(y => y.TotalTicks)).Take(10).ToList()
                .Select(z => new {Date = z.First().StartDate.ToUnixTime(), Ticks = z.Sum(a => a.TotalTicks)});

            var topTenFastestPeriods = _context.Records.OrderByDescending(x => x.MaxTicks).Take(10).ToList().Select(x=> new { Date = x.FastestInterval.ToUnixTime(), Ticks = x.MaxTicks});
            

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
