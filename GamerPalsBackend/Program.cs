using System;
using System.Threading;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Mongo;
using GamerPalsBackend.Other;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging.AzureAppServices.Internal;

namespace GamerPalsBackend
{
    public class Program
    {
        private static bool end = false;
        public static void Main(string[] args)
        {
            PalsConfiguration.UpdateSystemConfiguration();
            var updater = new Thread(SystemSettingsUpdater);
            updater.Start();
            var host  = BuildWebHost(args);
            host.Run();
            end = true;
            updater.Join();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static void SystemSettingsUpdater()
        {
            while (!end)
            {
                PalsConfiguration.UpdateSystemConfiguration();
                Console.WriteLine("Updated SystemSettings");
                Thread.Sleep(60 * 1000);
            }
        }
    }
}
