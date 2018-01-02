using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using HedgehogRun.Entities;
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
        public ActionResult Get()
        {
            JObject toReturn = new JObject();
            toReturn.Add("Test", "Hello, World!");
            return Content(toReturn.ToString(), MimeTypeHelper.ApplicationJson);
        }

        [HttpPost]
        public ActionResult Post()
        {
            using (var reader = new StreamReader(Request.Body))
            {
               var content = reader.ReadToEnd();
                var log = JsonConvert.DeserializeObject<HogLog>(content);
                return Content(JsonConvert.SerializeObject(log), MimeTypeHelper.ApplicationJson);

            }

        }
    }
}
