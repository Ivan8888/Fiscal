using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using Microsoft.Extensions.Configuration;
using WebShop.Services;
using WebShop.Filters;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Identity;
using WebShop.Models;
using System.Security.Claims;
using WebShop.AuthorizationReq;
using Microsoft.AspNetCore.Authorization;
using WebShop.Hubs;

namespace WebShop
{
    public class Startup
    {
        IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()/*.AddSessionStateTempDataProvider()*/;

            services.AddDbContext<ShopContext>(o => o
            .UseLazyLoadingProxies()
            .UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]));


            services.AddIdentity<AppUser, IdentityRole>(o => {
                o.User.RequireUniqueEmail = false;

                o.Lockout.AllowedForNewUsers = true;
                o.Lockout.MaxFailedAccessAttempts = 2;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(20);

                o.Password.RequireDigit = false;
                o.Password.RequiredLength = 4;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;

                o.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<ShopContext>();

            services.ConfigureApplicationCookie(o => {
                o.Cookie.Name = "name_of_authentication_cookie";
                o.ExpireTimeSpan = TimeSpan.FromMinutes(2);
                o.SlidingExpiration = false;
            });

            services.AddAuthorization(o => {
                o.AddPolicy("RequireEmail", p => p.RequireClaim(ClaimTypes.Email, "admin@gmail.com"));
                o.AddPolicy("JustRolesAdminSupport", p => p.RequireClaim(ClaimTypes.Role, "Admin", "Support"));

                //role admin or role user with email claim policy
                o.AddPolicy("AdminOrUserWithEmailAssertion", p => p.RequireAssertion(context =>
                    context.User.IsInRole("Admin") || (context.User.IsInRole("User") && context.User.HasClaim(c => c.Type == ClaimTypes.Email))
                ));

                //just user with more year can access
                o.AddPolicy("JustOlderThan18", p => p.AddRequirements(new AccessBasedOnUserYearRequiremnt(18)));

                //when one handler for requirement return failure, then don't check another handler for that requirement because requrement will fail
                o.InvokeHandlersAfterFailure = false;
            });

            services.AddSingleton<IAuthorizationHandler, AccessBasedOnUserYearHandler>();

            services.AddSingleton<IProductCount, ProductCount>();
            services.AddSingleton<ActionResultFilters>();

            services.AddCors(options => {
                options.AddPolicy("FromGoogle", p => p.WithOrigins("http://google.com", "https://google.com"));
            });

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            services.AddSession(o => {
                o.Cookie.Name = "name_of_session_cookie";
                o.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShopContext _dbcontext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //_dbcontext.Database.EnsureDeleted();
            if (_dbcontext.Database.EnsureCreated())
            {
                SeedUsersAndRoles.CreateInitialUsers(userManager, roleManager);
            }

            app.UseStatusCodePages();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseSession();

            app.UseCors("FromGoogle");

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(route => {
                route.MapHub<ChatHub>("/chathub");
            });

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                //endpoints.MapHub<ChatHub>("/chathub");

                endpoints.MapControllerRoute(
                    name: "AreaRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}",
                    dataTokens: new { locale = "route with int id" }
                    );
            });
        }
    }
}
