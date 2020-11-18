using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    //configure logging
                    webBuilder.ConfigureLogging((context, logging) => {
                        logging.AddConfiguration(context.Configuration.GetSection("Logging"));

                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            logging.AddConsole();
                            logging.AddFile("test_log.txt");
                        }
                    })
                    .UseStartup<Startup>();
                });
    }
}
