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
    public class TicksLastTwelveHours : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicksLastTwelveHours(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            var twelveHoursAgo = DateTime.UtcNow.AddHours(-12);
            var ticks = _context.HogLogs.Where(x => x.PostTime >= twelveHoursAgo).Sum(x => x.Ticks);
            

            JObject toReturn = new JObject();
            toReturn.Add("ticks", ticks);
            
            
            
            return Content(toReturn.ToString(), MimeTypeHelper.ApplicationJson);
        }      
    }
}
