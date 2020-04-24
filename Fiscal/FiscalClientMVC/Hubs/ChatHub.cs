using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FiscalClientMVC.Hubs
{
    public class ChatHub : Hub
    {
        public async Task MessageAll(string sender, string message)
        {
            await Clients.All.SendAsync("NewMessage", sender, message);
        }
    }
}
