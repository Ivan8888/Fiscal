using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<AppUser> _signInManager;
        UserManager<AppUser> _userManager;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            if (Request.Query.ContainsKey("ReturnUrl"))
            {
                ViewBag.ReturnUrl = Request.Query["ReturnUrl"];
            }
            else
            {
                ViewBag.ReturnUrl = "";
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);

                if (result.Succeeded)
                {
                    if (Request.Query.ContainsKey("ReturnUrl") && Request.Query["ReturnUrl"] != "")
                    {
                        return Redirect(Request.Query["ReturnUrl"]);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    UserName = model.UserName
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
