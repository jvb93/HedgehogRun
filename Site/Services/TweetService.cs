using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;

namespace HedgehogRun.Services
{
    public class TweetService : ITweetService
    {
        IConfiguration _configuration;

        public TweetService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void SendTweet(string tweet)
        {
            try
            {
                string consumerKey =_configuration["TwitterConsumerKey"];
                string consumerSecret = _configuration["TwitterConsumerSecret"];
                string accessToken = _configuration["TwitterAccessToken"];
                string accessTokenSecret = _configuration["TwitterAccessTokenSecret"];

                var client = new RestClient("https://api.twitter.com");

                // The OAuth keys/tokens/secrets are retrieved elsewhere
                client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                    consumerKey, consumerSecret, accessToken, accessTokenSecret
                );
                //status = HttpUtility.UrlEncode(status);
                var request = new RestRequest("/1.1/statuses/update.json", Method.POST);
                request.AddParameter("status", tweet, ParameterType.GetOrPost);

                var response = client.Execute(request);
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}
