using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Authorization
{
    public class AccessBasedOnCustomerCountRequirements : IAuthorizationRequirement
    {
        public int max_customer { get; private set; }
        public AccessBasedOnCustomerCountRequirements(int max_customer)
        {
            this.max_customer = max_customer;
        }
    }
}
