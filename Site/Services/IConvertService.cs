using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgehogRun.Services
{
    public interface IConvertService
    {
        double ConvertTicksToMiles(int ticks);
        double ConvertTicksToMilesPerHour(int ticks);
    }
}
