using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgehogRun.Services
{
    public class ConvertService :IConvertService
    {
        public double ConvertTicksToMiles(int ticks)
        {
            return ((ticks * 2 * 3.14 * 5.25) / 12) / 5280;
        }

        public double ConvertTicksToMilesPerHour(int ticks)
        {
            var distance = ((ticks * 2 * 3.14 * 5.25) / 12);
            return distance * 0.0113636;
        }
    }
}

