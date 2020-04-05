using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiscalClientMVC.Models;
using System.Security.Claims;

namespace FiscalClientMVC.Data
{
    public class SeedUsersAndRoles
    {
        public static void CreateInitialUsers(UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            IdentityRole admin = new IdentityRole("Administrator");
            IdentityRole support = new IdentityRole("Support");
            IdentityRole user = new IdentityRole("User");
            _roleManager.CreateAsync(admin).Wait();
            _roleManager.CreateAsync(support).Wait();
            _roleManager.CreateAsync(user).Wait();

            AppUser temp_user;

            AppUser admin_user = new AppUser()
            {
                FirstName = "Ivan",
                LastName = "Milutinovic",
                Email = "admin@gmail.com",
                Age = 31,
                UserName = "admin_user",
            };
            _userManager.CreateAsync(admin_user, "adminuser").Wait();
            temp_user =  _userManager.FindByNameAsync(admin_user.FirstName).Result;
            _userManager.AddToRoleAsync(temp_user, admin.Name).Wait();

            AppUser support_user = new AppUser()
            {
                FirstName = "Zoran",
                LastName = "Zoranovic",
                Email = "support@gmail.com",
                Age = 25,
                UserName = "support_user",
            };
            _userManager.CreateAsync(support_user, "supportuser").Wait();
            temp_user = _userManager.FindByNameAsync(support_user.FirstName).Result;
            _userManager.AddToRoleAsync(temp_user, support.Name).Wait();

            AppUser user17 = new AppUser()
            {
                FirstName = "Marko",
                LastName = "Markovic",
                Email = "marko@gmail.com",
                Age = 17,
                UserName = "user17",
            };
            _userManager.CreateAsync(user17, "user17").Wait();
            temp_user = _userManager.FindByNameAsync(user17.FirstName).Result;
            _userManager.AddToRoleAsync(temp_user, user.Name).Wait();

            AppUser user21 = new AppUser()
            {
                FirstName = "Janko",
                LastName = "Jankovic",
                Email = "",
                Age = 21,
                UserName = "user21",
            };
            _userManager.CreateAsync(user21, "user21").Wait();
            temp_user = _userManager.FindByNameAsync(user21.FirstName).Result;
            _userManager.AddToRoleAsync(temp_user, user.Name).Wait();

            var users_list = _userManager.Users.ToList();
            //add claims
            foreach(var iteam in users_list)
            {
                if (!string.IsNullOrWhiteSpace(iteam.Email))
                {
                    _userManager.AddClaimAsync(iteam, new Claim(ClaimTypes.Email, iteam.Email)).Wait();
                }
            }
        }
    }
}
