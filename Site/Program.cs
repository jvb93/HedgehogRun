using System;
using Hangfire;
using HedgehogRun.Hangfire;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HedgehogRun
{
    public class Program
    {

      

        public static void Main(string[] args)
        {
            
            BuildWebHost(args).Run();
           
        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>()
               .Build();
    }
}
