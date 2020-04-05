using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FiscalClientMVC.ViewModels;
using FiscalClientMVC.Models;

namespace FiscalClientMVC.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AdministratorController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult ListRoles()
        {
            IQueryable<IdentityRole> roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = model.RoleName;
                IdentityResult result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListRoles));
                }
                else
                {
                    foreach (var iteam in result.Errors)
                    {
                        ModelState.AddModelError("", iteam.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                EditRoleViewModel view_model = new EditRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                List<AppUser> all_users = _userManager.Users.ToList();
                foreach (var user in all_users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        view_model.Users.Add(user.UserName);
                    }
                }

                return View(view_model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IdentityRole role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = model.RoleName;
                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(roleId);
            if(role == null)
            {
                return NotFound();
            }

            ViewBag.RoleId = roleId;

            List<EditUsersInRoleViewModel> view_model_list = new List<EditUsersInRoleViewModel>();
            List<AppUser> users = _userManager.Users.ToList();

            foreach(var user in users)
            {
                EditUsersInRoleViewModel view_model = new EditUsersInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                view_model.isSelected = await _userManager.IsInRoleAsync(user, role.Name);

                view_model_list.Add(view_model);
            }

            return View(view_model_list);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<EditUsersInRoleViewModel> view_model, string roleId)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(roleId); 

            if(role == null)
            {
                return NotFound();
            }

            foreach(var iteam in view_model)
            {
                AppUser user = await _userManager.FindByNameAsync(iteam.UserName);

                if(user == null)
                {
                    return NotFound();
                }

                bool is_in_role = await _userManager.IsInRoleAsync(user, role.Name);
                
                if(iteam.isSelected && !is_in_role)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!iteam.isSelected && is_in_role)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
            }

            return RedirectToAction("EditRole", new { id = roleId });
        }
    }
}