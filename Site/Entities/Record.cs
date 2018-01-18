using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgehogRun.Entities
{
    public class Record 
    {
        public long Id { get; set; }
        public int MaxTicks { get; set; }
        public int TotalTicks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime FastestInterval { get; set; }
    }
}
