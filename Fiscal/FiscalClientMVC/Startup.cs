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
                identityOptions.User.RequireUniqueEmail = false;

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 2;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

                identityOptions.Password.RequiredLength = 4;
                identityOptions.Password.RequiredUniqueChars = 2;
                identityOptions.Password.RequireDigit = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireLowercase = true;

                identityOptions.SignIn.RequireConfirmedEmail = false;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<FiscalContext>();

            services.ConfigureApplicationCookie(cookieAuthtenticationOptions => {
                cookieAuthtenticationOptions.Cookie.Name = "AuthenticationCookie";
                cookieAuthtenticationOptions.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                cookieAuthtenticationOptions.SlidingExpiration = false;
            });

            services.AddAuthorization(authorizationOptions => {
                authorizationOptions.AddPolicy("CanDeleteCustomer", p => p.RequireRole("User")
                                                          .RequireClaim(ClaimTypes.Email));
                authorizationOptions.AddPolicy("RequireEmail", p => p.RequireClaim(ClaimTypes.Email));
            });

            services.AddSingleton<ICustomerCount, CustomerCountService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, FiscalContext context, ILogger<Startup> logger, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated())
            {
                SeedUsersAndRoles.CreateInitialUsers(userManager, roleManager, signInManager);
            }

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
