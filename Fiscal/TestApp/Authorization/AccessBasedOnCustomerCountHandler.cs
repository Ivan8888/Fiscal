using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TestApp.Services;

namespace TestApp.Authorization
{
    public class AccessBasedOnCustomerCountHandler : AuthorizationHandler<AccessBasedOnCustomerCountRequirements>
    {
        ICustomerCount _customerCount;
        public AccessBasedOnCustomerCountHandler(ICustomerCount customerCount)
        {
            _customerCount = customerCount;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessBasedOnCustomerCountRequirements requirement)
        {
            if(context.User == null)
            {
                return Task.CompletedTask;
            }

            if(context.User.HasClaim(c => c.Type == "AgeClaim" && Convert.ToInt32(c.Value) > 18) && context.User.HasClaim(ClaimTypes.Role, "User") && _customerCount.GetCustomerNumber() < requirement.max_customer)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
