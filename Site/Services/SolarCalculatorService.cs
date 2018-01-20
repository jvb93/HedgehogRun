using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Innovative.Geometry;
using Innovative.SolarCalculator;

namespace HedgehogRun.Services
{
    public class SolarCalculatorService : ISolarCalculatorService
    {
        public DateTime GetSunsetTimeForLatLong(DateTime date, TimeZoneInfo timeZone, decimal latitude, decimal longitude)
        {
            SolarTimes solarTimes = new SolarTimes(date, new Angle(latitude), new Angle(longitude));

            return solarTimes.Sunset;
        }

        public DateTime GetSunriseTimeForLatLong(DateTime date, TimeZoneInfo timeZone, decimal latitude, decimal longitude)
        {
            SolarTimes solarTimes = new SolarTimes(date, timeZone.BaseUtcOffset.Hours, new Angle(latitude), new Angle(longitude));

            return solarTimes.Sunrise;
        }
    }
}
