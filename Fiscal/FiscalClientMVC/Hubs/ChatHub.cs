using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using FiscalClientMVC.Models;
using FiscalClientMVC.Services;

namespace FiscalClientMVC.Hubs
{
    public class ChatHub : Hub
    {
        UserManager<AppUser> _userManager;
        HubUserService _hubUserService;
        public ChatHub(UserManager<AppUser> userManager, HubUserService hubUserService)
        {
            _userManager = userManager;
            _hubUserService = hubUserService;
        }

        public override Task OnConnectedAsync()
        {
            HubUserInfo u = GetHubUser();
            if (u == null)
            {
                HubUserInfo user = new HubUserInfo();
                user.full_name = GetIdentityUserFullName().Result;
                user.connection_ids.Add(Context.ConnectionId);
                user.user_id = Context.UserIdentifier;

                _hubUserService.HubUsers.Add(user);
            }
            else
            {
                //user exist, just add new connection id to him
                u.connection_ids.Add(Context.ConnectionId);
                //add this connection_id to all groups that is this user alreday in
                foreach(var group_name in u.groups)
                {
                    Groups.AddToGroupAsync(Context.ConnectionId, group_name);
                }
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            HubUserInfo user = GetHubUser();
            if(user != null)
            {
                user.connection_ids.Remove(Context.ConnectionId);
                if(user.connection_ids.Count() == 0)
                {
                    _hubUserService.HubUsers.Remove(user);
                    foreach (string group in user.groups)
                    {
                        Clients.Group(group).SendAsync("GroupChange", group, user.full_name, "disconected from");
                    }
                }
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task MessageAll(string message, string group_name)
        {
            HubUserInfo user = GetHubUser();
            if (user.groups.Contains(group_name))
            {
                await Clients.Group(group_name).SendAsync("NewMessage", user.full_name, message, group_name);
            }
        }

        public async Task JoinGroup(string group_name)
        {
            HubUserInfo user = GetHubUser();
            //just if alreday not in that group
            if (!user.groups.Contains(group_name))
            {
                user.groups.Add(group_name);
                foreach (var id in user.connection_ids)
                {
                    await Groups.AddToGroupAsync(id, group_name);
                }

                await Clients.Group(group_name).SendAsync("GroupChange", group_name, user.full_name, "join");
            }
        }

        public async Task LeaveGroup(string group_name)
        {
            HubUserInfo user = GetHubUser();
            //just if it is alreday in that group
            if (user.groups.Contains(group_name))
            {
                user.groups.Remove(group_name);
                foreach(var id in user.connection_ids)
                {
                    await Groups.RemoveFromGroupAsync(id, group_name);
                }
                
                await Clients.Group(group_name).SendAsync("GroupChange", group_name, user.full_name, "leave");
            }
        }

        #region "Helper methods"
        private HubUserInfo GetHubUser()
        {
            return _hubUserService.HubUsers.Where(u => u.user_id == Context.UserIdentifier).SingleOrDefault();
        }

        private async Task<string> GetIdentityUserFullName()
        {
            AppUser user = await _userManager.GetUserAsync(Context.User);
            string full_name = "AnonimusUser";

            if (user != null)
            {
                full_name = $"{user.FirstName} {user.LastName}";
            }

            return full_name;
        }
        #endregion
    }
}
