using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FiscalClientMVC.Security
{
    public class AccessBasedOnYearRequirement : IAuthorizationRequirement
    {
        public readonly int MinYear;
        public AccessBasedOnYearRequirement(int min_year)
        {
            MinYear = min_year;
        }
    }
}
