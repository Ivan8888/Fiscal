using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using FiscalClientMVC.Models;

namespace FiscalClientMVC.Hubs
{
    public class ChatHub : Hub
    {
        IHttpContextAccessor _httpContextAccessor;
        UserManager<AppUser> _userManager;
        public ChatHub(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task MessageAll(string message)
        {
            AppUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            string full_name = "AnonimusUser";

            if (user != null)
            {
                full_name = $"{user.FirstName} {user.LastName}";
            }
            await Clients.All.SendAsync("NewMessage", full_name , message);
        }
    }
}
