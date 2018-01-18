﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dotnet
{
    public class Program
    {
        private static readonly string[] RequiredEnvVars = new [] {"LIGHTRAIL_API_KEY", "LIGHTRAIL_SHARED_SECRET", "STRIPE_API_KEY", "STRIPE_PUBLISHABLE_KEY", "TITLE", "SHOPPER_ID", "ORDER_TOTAL"};

        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "..", "shared", ".env"));
            foreach (var requiredVar in RequiredEnvVars)
            {
                if (System.Environment.GetEnvironmentVariable(requiredVar) == null)
                {
                    Console.Error.WriteLine("One or more environment variables necessary to run this demo is/are not set.  See README.md on setting these values.");
                    return;
                }
            }

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 3000);
                })
                .Build();
    }
}
