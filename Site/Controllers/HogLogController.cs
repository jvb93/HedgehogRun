using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using HedgehogRun.Entities;
using HedgehogRun.EntityFramework;
using HedgehogRun.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HedgehogRun.Controllers
{
    [Produces("application/json")]
    [Route("api/HogLog")]
    public class HogLogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HogLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Get()
        {
           
           
            return Content(JsonConvert.SerializeObject(_context.HogLogs.ToList()), MimeTypeHelper.ApplicationJson);
        }

        [HttpPost]
        public ActionResult Post()
        {
           
                using (var reader = new StreamReader(Request.Body))
                {
                    var content = reader.ReadToEnd();
                    var log = JsonConvert.DeserializeObject<HogLog>(content);
                    _context.HogLogs.Add(log);
                    _context.SaveChanges();
                    return Content(JsonConvert.SerializeObject(log), MimeTypeHelper.ApplicationJson);

                }
            
         
          

        }
    }
}
