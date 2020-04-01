using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FiscalClientMVC {
    public class Program {
        public static void Main(string[] args) {
            IHost host = CreateHostBuilder(args).UseDefaultServiceProvider(o => o.ValidateScopes = false).Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("In main method");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IConfiguration build_config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
                

            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.AddConfiguration(build_config);
                })
                .ConfigureLogging((hostBuilderContext, iLoggingBuilder) => {
                    iLoggingBuilder.AddConfiguration(hostBuilderContext.Configuration.GetSection("Logging"));

                    if (hostBuilderContext.HostingEnvironment.IsDevelopment())
                    {
                        iLoggingBuilder.ClearProviders();
                        iLoggingBuilder.AddDebug();
                        iLoggingBuilder.AddFile("log.txt", minimumLevel: LogLevel.Debug);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
