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
    public class CurrentSpeed : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrentSpeed(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {

            var current = _context.HogLogs.OrderByDescending(x => x.PostTime).FirstOrDefault();
            var max = _context.HogLogs.Max(x => x.Ticks);

            JObject toReturn = new JObject();
            toReturn.Add("current", current?.Ticks ?? 0);
            toReturn.Add("max", max);
            
            
            return Content(toReturn.ToString(), MimeTypeHelper.ApplicationJson);
        }      
    }
}
