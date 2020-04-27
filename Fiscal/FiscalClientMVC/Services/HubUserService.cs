using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiscalClientMVC.Hubs;

namespace FiscalClientMVC.Services
{
    public class HubUserService
    {
        public List<HubUserInfo> HubUsers { get; }
        public HubUserService()
        {
            HubUsers = new List<HubUserInfo>();
        }
    }
}
