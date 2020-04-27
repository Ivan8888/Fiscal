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
using FiscalClientMVC.Security;
using Microsoft.AspNetCore.Authorization;
using FiscalClientMVC.Hubs;
using System.IO;
using Microsoft.Extensions.FileProviders;

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
                cookieAuthtenticationOptions.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                //cookieAuthtenticationOptions.Cookie.Expiration
                cookieAuthtenticationOptions.SlidingExpiration = false;
            });

            services.AddAuthorization(authorizationOptions => {

                authorizationOptions.AddPolicy("CanView", p => p.RequireAssertion(context =>
                    context.User.IsInRole("Admin") || context.User.IsInRole("Support") || context.User.IsInRole("User")));

                //authorizationOptions.AddPolicy("CanView", p => p.RequireRole("Admin", "Support", "User"));

                authorizationOptions.AddPolicy("CanCreate", p => p.RequireAssertion(context =>
                    context.User.IsInRole("Admin") || context.User.IsInRole("Support")));

                authorizationOptions.AddPolicy("CanEdit", p => p.RequireClaim(ClaimTypes.Role, "Admin"));
                authorizationOptions.AddPolicy("CanDelete", p => p.RequireRole("Admin"));

                authorizationOptions.AddPolicy("JustForAdult", p => p.AddRequirements(new AccessBasedOnYearRequirement(17)));

                authorizationOptions.AddPolicy("CanUpdateRole", p => p.AddRequirements(new UserEditRoleRequirement()));

            });

            services.AddSingleton<IAuthorizationHandler, UserEditRoleHandler>();

            services.AddSingleton<IAuthorizationHandler, AccessBasedOnYearHandler>();

            services.AddSingleton<ICustomerCount, CustomerCountService>();
            services.AddMvc();

            services.AddCors(o => o.AddPolicy("FromGoogle", p => p.WithOrigins("www.google.com")));

            services.AddDistributedMemoryCache();
            services.AddSession(sessionOptions => {
                sessionOptions.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            services.AddMemoryCache();

            services.AddSignalR(hubOpitons => {
                hubOpitons.HandshakeTimeout = TimeSpan.FromSeconds(30);
                hubOpitons.KeepAliveInterval = TimeSpan.FromSeconds(30);
                hubOpitons.EnableDetailedErrors = true;
            });

            services.AddSingleton<HubUserService>();
        }

        public void Configure(IApplicationBuilder app, FiscalContext context, ILogger<Startup> logger, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, IWebHostEnvironment environment)
        {
            //context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated())
            {
                SeedUsersAndRoles.CreateInitialUsers(userManager, roleManager, signInManager);
            }

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(environment.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });

            app.UseNodeModules();

            //app.UseCookiePolicy();
            app.UseSession();

            app.UseCors();

            app.UseAuthentication();

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(hubRootBuilder => {
                hubRootBuilder.MapHub<ChatHub>("/chathub");
            });

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
