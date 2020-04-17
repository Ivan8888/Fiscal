using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FiscalClientMVC.Security
{
    public class AccessBasedOnYearHandler : AuthorizationHandler<AccessBasedOnYearRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessBasedOnYearRequirement requirement)
        {
            if(context.User == null)
            {
                return Task.CompletedTask;
            }

            Claim claim =  context.User.FindFirst("AgeClaim");
            if(claim == null)
            {
                return Task.CompletedTask;
            }

            int user_age;
            if(int.TryParse(claim.Value, out user_age))
            {
                if (user_age >= requirement.MinYear)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
