using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using TestApp.Data;
using TestApp.Filters;
using TestApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using TestApp.Models;
using WebShop.Data;
using System.Security.Claims;
using TestApp.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace TestApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SiteContext>(builder =>
            builder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>(o => {
                o.Password.RequiredLength = 4;
                o.Password.RequireDigit = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;

                o.Lockout.AllowedForNewUsers = true;
                o.Lockout.MaxFailedAccessAttempts = 2;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
            })
                .AddEntityFrameworkStores<SiteContext>();

            services.ConfigureApplicationCookie(o => {
                o.Cookie.Name = "my_authentication_cookie";
                o.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                o.SlidingExpiration = false;
            });

            services.AddAuthorization(o => {
                o.AddPolicy("RequireEmail", p => p.RequireClaim(ClaimTypes.Email));
                o.AddPolicy("RequireAdminEmail", p => p.RequireClaim(ClaimTypes.Email, "admin@gmail.com"));

                //the same result, the role is claim of type ClaimTypes.Role
                //o.AddPolicy("RequireSupportRole", p => p.RequireRole("Support"));
                o.AddPolicy("RequireSupportRole", p => p.RequireClaim(ClaimTypes.Role, "Support"));

                //require admin role or user role with email claim
                o.AddPolicy("AdminOrUserWithEmail", p => p.RequireAssertion(context =>
                    context.User.HasClaim(ClaimTypes.Role, "Admin") ||
                    (context.User.HasClaim(ClaimTypes.Role, "User") && context.User.HasClaim(c => c.Type == ClaimTypes.Email && c.Value.Contains("user")))));

                //adult users
                o.AddPolicy("JustAdultUsers", o => o.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "AgeClaim" && Convert.ToInt32(c.Value) >= 18) && context.User.HasClaim(ClaimTypes.Role, "User")
                    ));

                //just adult with user role and if current count of customer lower than value passed in constructor
                o.AddPolicy("AdultUserAndBasedOnCustomerCount", o => o.AddRequirements(new AccessBasedOnCustomerCountRequirements(3)));

                //when one handler for requirement return failure, then don't check another handler for that requirement because requrement will fail
                o.InvokeHandlersAfterFailure = false;
            });

            services.AddCors(o => {
                o.AddPolicy("FromGoogle", b => b.WithOrigins("https://www.google.com"));
            });

            services.AddScoped<IAuthorizationHandler, AccessBasedOnCustomerCountHandler>();

            services.AddScoped<ICustomerCount, CustomerCount>();
            services.AddScoped<ActionResultFilter>();

            services.AddMemoryCache();

            services.AddMvc().AddSessionStateTempDataProvider();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.Name = "session_cookie";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, SiteContext dbcontext, IWebHostEnvironment env, ILogger<Startup> logger, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            logger.LogInformation($"Work environment is: {env.EnvironmentName}");

            app.UseCors("FromGoogle");

            app.UseSession();

            //app.UseCors(p => {
            //    p.WithOrigins("https://www.google.com");
            //});

            if (env.IsDevelopment())
            {
                dbcontext.Database.EnsureDeleted();
                dbcontext.Database.EnsureCreated();
                SeedUsersAndRoles.CreateInitialUsers(userManager, roleManager);
            }
            else
            {
                dbcontext.Database.EnsureCreated();
            }

            app.UseAuthentication();

            app.UseStatusCodePages(); 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles(new StaticFileOptions() {
                RequestPath = "/wwwroot"
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFolder")),
                RequestPath = "/staticfolder"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(a =>
            a.MapDefaultControllerRoute());

            //app.Run(async (context) => {
            //    await Task.Run(() => context.Response.StatusCode = 500);
            //});

            //app.Run(async (context) => {
            //    await Task.Run(() => throw new NotImplementedException());
            //});

            app.Run(async (context) => {
                string response = "This is response!!!";
                await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(response));
            });
        }
    }
}
