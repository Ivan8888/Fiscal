using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiscalClientMVC.Security
{
    public class UserCanNotEditDepandingRoleAuthorizationHandler : AuthorizationHandler<UserEditRoleAuthorizationRequirement>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserCanNotEditDepandingRoleAuthorizationHandler(IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _roleManager = roleManager;

        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserEditRoleAuthorizationRequirement requirement)
        {   
            string role_id = (string)_httpContextAccessor.HttpContext.Request.RouteValues["id"];
            IdentityRole role = _roleManager.FindByIdAsync(role_id).Result;
            bool in_role = context.User.IsInRole(role.Name);

            if (!in_role)
            {
                context.Succeed(requirement);
            }

           return Task.CompletedTask;
        }
    }
}
