using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FiscalServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using FiscalServer.CustomFormaters.Output;

namespace FiscalServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FiscalContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            //services.AddControllers(mvcOptions =>
            //mvcOptions.OutputFormatters.Add(new XmlSerializerOutputFormatter()));

            services.AddMvc(o => {
                o.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                o.OutputFormatters.Add(new CustomProductOutputFormater());

                o.InputFormatters.Add(new XmlSerializerInputFormatter(o));
            });

            //services.AddControllers().AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FiscalContext dbcontext)
        {
            dbcontext.Database.EnsureDeleted();
            dbcontext.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
