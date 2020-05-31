using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Config").ToString())
            //    .AddXmlFile("config.xml")
            //    .AddJsonFile("config.json")
            //    .AddJsonFile("appsettings.json");

            //IConfiguration configuration = builder.Build();

            return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder
                    //.UseKestrel(options => {
                    //    options.Limits.MaxConcurrentConnections = 100;
                    //    options.Limits.MaxConcurrentUpgradedConnections = 50;
                    //    options.Limits.MaxRequestBodySize = 2 * 1024 * 1024;
                    //    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(3);
                    //})
                //.UseHttpSys(options => {
                //    options.Authentication.Schemes = Microsoft.AspNetCore.Server.HttpSys.AuthenticationSchemes.None;
                //    options.MaxConnections = 50;
                //    options.MaxRequestBodySize = 2 * 1024 * 1024;
                //    options.UrlPrefixes.Add("http://localhost:5250");
                //})
                //.UseConfiguration(configuration)
                .ConfigureLogging((context, logging) => {
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.ClearProviders();
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddDebug();
                        logging.AddFile("Log_{Date}.txt");
                    }
                    else
                    {
                        logging.AddDebug();
                        logging.AddFile("Log_{Date}.txt");
                    }
                })
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .UseStartup<Startup>();
            });
        }
    }
}
