using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgehogRun.Entities
{
    public class HogLog 
    {
        public long Id { get; set; }
        public int Ticks { get; set; }
        public double TemperatureF { get; set; }
        public double Humidity { get; set; }
    }
}
