using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => {
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            })
                .AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<FiscalContext>(options =>options
            //setting for lazy loading
            //.UseLazyLoadingProxies()
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env, FiscalContext fiscalContext)
        {
            //fiscalContext.Database.EnsureDeleted();
            //fiscalContext.Database.EnsureCreated();

            app.UseStaticFiles(new StaticFileOptions() {
                RequestPath = "/root"
            });

            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFolder")), 
                RequestPath = "/static"
            });

            //app.Use(async (context, next) => {
            //    await context.Response.WriteAsync($"Path is: {context.Request.Path.Value}   ");
            //    await next.Invoke();
            //});

            app.Map("/map", (aplicationBuilder) => {
                aplicationBuilder.Use(async (c, next) => {
                    await c.Response.WriteAsync("Inside use middleware, in map");
                    await next.Invoke();
                });

                //aplicationBuilder.Run(async (c) => {
                //    await c.Response.WriteAsync("Inside run middleware, in map");
                //});
            });

            //app.Run(async (context) => {
            //    await context.Response.WriteAsync("Inside run middleware, not in map! ");
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
