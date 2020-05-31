using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebShop.AuthorizationReq
{
    public class AccessBasedOnUserYearHandler : AuthorizationHandler<AccessBasedOnUserYearRequiremnt>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessBasedOnUserYearRequiremnt requirement)
        {
            if(context.User == null)
            {
                return Task.CompletedTask;
            }

            if(context.User.HasClaim(c => c.Type == "AgeClaim" && Convert.ToInt32(c.Value) >= requirement.MinYear))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
