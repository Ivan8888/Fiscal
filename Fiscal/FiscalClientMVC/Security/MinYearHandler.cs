using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FiscalClientMVC.Security
{
    public class MinYearHandler : AuthorizationHandler<MinYearRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinYearRequirement requirement)
        {
            Claim claim = context.User.FindFirst("YearClaim");
            if(claim == null)
            {
                return Task.CompletedTask;
            }

            int user_year;
            if(int.TryParse(claim.Value, out user_year))
            {
                if(user_year >= requirement.MinYear)
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                return Task.CompletedTask;
            }


            return Task.CompletedTask;
        }
    }
}
