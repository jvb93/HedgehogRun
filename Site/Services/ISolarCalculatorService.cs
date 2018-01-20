using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgehogRun.Services
{
    public interface ISolarCalculatorService
    {
        DateTime GetSunsetTimeForLatLong(DateTime date, TimeZoneInfo timeZone, decimal latitude, decimal longitude);
        DateTime GetSunriseTimeForLatLong(DateTime date, TimeZoneInfo timeZone, decimal latitude, decimal longitude);
    }
}
