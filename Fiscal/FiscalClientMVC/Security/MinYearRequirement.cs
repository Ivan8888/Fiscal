using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FiscalClientMVC.Security
{
    public class MinYearRequirement : IAuthorizationRequirement
    {
        public readonly int MinYear;
        public MinYearRequirement(int year)
        {
            MinYear = year;
        }
    }
}
