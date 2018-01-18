using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgehogRun.Services
{
    public interface ITweetService
    {
        void SendTweet(string tweet);
        
    }
}
