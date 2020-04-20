using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiscalClientMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FiscalClientMVC.ViewModels;
using System.Security;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FiscalClientMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IOptions<CookieAuthenticationOptions> _cookieAuthenticationOptions;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<IdentityOptions> identityOptions, IOptions<CookieAuthenticationOptions> cookieAuthenticationOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityOptions = identityOptions;
            _cookieAuthenticationOptions = cookieAuthenticationOptions;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser app_user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Age = model.Age,
                    UserName = model.UserName
                };

                IdentityResult result = await _userManager.CreateAsync(app_user, model.Password);
                if (result.Succeeded)
                {
                    AppUser user = await _userManager.FindByNameAsync(app_user.UserName);

                    if (!string.IsNullOrWhiteSpace(user.Email))
                    {
                        Claim claim = new Claim(ClaimTypes.Email, user.Email);
                        await _userManager.AddClaimAsync(user, claim); 
                    }

                    _userManager.AddClaimAsync(user, new Claim("AgeClaim", Convert.ToString(user.Age))).Wait();

                    return RedirectToAction(nameof(Login));
                }
            }

            return View(model);
        }

        [HttpGet]
        public ViewResult Login()
        {
            //if (Request.Query.ContainsKey("ReturnUrl"))
            //{
            //    ViewBag.ReturnUrl = Request.Query["ReturnUrl"].First();
            //}

            if (Request.Query.ContainsKey("ReturnUrl"))
            {
                TempData["ReturnUrl"] = Request.Query["ReturnUrl"].First();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent:true, lockoutOnFailure:true);
                if (result.Succeeded)
                {
                    //if (Request.Query.Keys.Contains("ReturnUrl"))
                    //{
                    //    return Redirect(Request.Query["ReturnUrl"].First());
                    //}
                    //else
                    //{
                    //    return RedirectToAction("Index", "Home");
                    //}

                    object returnUrl = TempData["ReturnUrl"];


                    if (returnUrl != null)
                    {
                        //return Redirect(302), TempData only deleted when return OK(200), must be manualy remove in this case
                        TempData.Remove("ReturnUrl");
                        return Redirect((string)returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    string error_desc = GetLoginError(result);
                    ModelState.AddModelError("", error_desc);
                }
            }

            return View();
        }

        public async Task<IActionResult>  Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private string GetLoginError(Microsoft.AspNetCore.Identity.SignInResult result)
        {
            if (result.IsLockedOut)
            {
                return $"User is locked out for: {_identityOptions.Value.Lockout.DefaultLockoutTimeSpan.TotalMinutes} minutes";
            }
            else if(result.IsNotAllowed)
            {
                return "Not allowed to sign in";
            }
            else if(result.RequiresTwoFactor)
            {
                return "Requires two factor autentification";
            }
            else
            {
                return "Probably wrong user name or password, try again.";
            }
        }
    }
}