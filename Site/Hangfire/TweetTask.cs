using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HedgehogRun.EntityFramework;
using HedgehogRun.Services;

namespace HedgehogRun.Hangfire
{
    public class TweetTask : ITweetTask
    {
        private readonly ITweetService _tweetService;
        private readonly IConvertService _convertService;
        private readonly ApplicationDbContext _applicationDbContext;

        public TweetTask(ITweetService tweetService, ApplicationDbContext applicationDbContext, IConvertService convertService)
        {
            _tweetService = tweetService;
            _applicationDbContext = applicationDbContext;
            _convertService = convertService;
        }

        public void Execute()
        {
            var today = DateTime.UtcNow.Date;
            var latest = _applicationDbContext.Records.Where(x => x.EndDate > today);
            if (latest.Any())
            {
                var miles =   _convertService.ConvertTicksToMiles(latest.First().TotalTicks);
                _tweetService.SendTweet($"Zoom! I ran {miles:F} miles last night! How about you?");
            }
        }
    }
}
