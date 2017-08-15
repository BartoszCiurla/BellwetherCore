using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Autofac.Extensions.DependencyInjection;

namespace Bellwether.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var environmentName = "";

            return WebHost.CreateDefaultBuilder(args)
                   .ConfigureServices(services => services.AddAutofac())
                   .UseEnvironment(environmentName)
                   .UseStartup<Startup>()
                   .Build();
        }
    }
}
