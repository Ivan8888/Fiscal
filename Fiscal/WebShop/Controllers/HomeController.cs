using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            }

            return View();
        }
    }
}