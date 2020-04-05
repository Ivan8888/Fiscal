using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FiscalClientMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FiscalClientMVC.Services;
using Microsoft.Extensions.Logging;
using FiscalClientMVC.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FiscalClientMVC
{
    public class Startup
    {
        IConfiguration _config;
        IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FiscalContext>(
                option => option.UseSqlServer(_config.GetConnectionString("Default")));

            services.AddIdentity<AppUser, IdentityRole>(identityOptions => {
                identityOptions.User.RequireUniqueEmail = true;

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 3;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

                identityOptions.Password.RequiredLength = 4;
                identityOptions.Password.RequiredUniqueChars = 2;
                identityOptions.Password.RequireDigit = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireLowercase = true;

                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = false;
             })
                .AddEntityFrameworkStores<FiscalContext>();

            services.ConfigureApplicationCookie(cookieAuthtenticationOptions => {
                cookieAuthtenticationOptions.Cookie.Name = "AuthenticationCookie";
                cookieAuthtenticationOptions.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                cookieAuthtenticationOptions.SlidingExpiration = false;
            });

            services.AddAuthorization(o =>
            o.AddPolicy("EmailPolicy", p => p.RequireClaim(ClaimTypes.Email))
            );

            services.AddSingleton<ICustomerCount, CustomerCountService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, FiscalContext context, ILogger<Startup> logger, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            SeedUsersAndRoles.CreateInitialUsers(userManager, roleManager);

            app.UseAuthentication();

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
