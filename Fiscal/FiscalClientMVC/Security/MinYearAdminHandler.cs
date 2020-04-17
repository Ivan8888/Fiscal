using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FiscalClientMVC.Security
{
    public class MinYearAdminHandler : AuthorizationHandler<MinYearRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinYearRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
