using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebShop.Hubs
{
    public class ChatHub : Hub
    {
        const string group_name = "ChatGroup";
        private static Dictionary<string, string> _connectedUsers = new Dictionary<string, string>();
        public async Task MessageAll(string message)
        {
            var sender = _connectedUsers[Context.ConnectionId];
            await Clients.Group(group_name).SendAsync("NewMessage", sender , message);
        }

        public async Task JoinGroup(string sender)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group_name);
            _connectedUsers.Add(Context.ConnectionId, sender);
            await Clients.Group(group_name).SendAsync("GroupChange", _connectedUsers.Values);
        }

        public async Task LeaveGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group_name);
            _connectedUsers.Remove(Context.ConnectionId);
            await Clients.Group(group_name).SendAsync("GroupChange", _connectedUsers.Values);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connectedUsers.Remove(Context.ConnectionId);
            Clients.Group(group_name).SendAsync("GroupChange", _connectedUsers.Values);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
