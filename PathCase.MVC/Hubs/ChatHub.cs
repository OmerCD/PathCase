using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PathCase.Core.ValueObjects;
using PathCase.Infrastructure.Services;
using PathCase.Infrastructure.Services.Interfaces;
using PathCase.MVC.Extensions;
using PathCase.MVC.Hubs.Client;

namespace PathCase.MVC.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly ChatGroupManager _groupManager;
        private readonly IChatRoomService _chatRoomService;

        public ChatHub(ChatGroupManager groupManager, IChatRoomService chatRoomService)
        {
            _groupManager = groupManager;
            _chatRoomService = chatRoomService;
        }

        public async Task JoinGroup(string groupName)
        {
            var userName = Context.User.GetName();
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            _groupManager.AddUser(groupName, new UserInfo(userName, Context.ConnectionId));
            await Clients.Group(groupName).ServerMessage($"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _groupManager.RemoveUser(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).ServerMessage($"{Context.ConnectionId} has left the group {groupName}.");
        }

        public async Task SendMessage(string groupName, string message)
        {
            var userName = Context.User.GetName();
            await Clients.Group(groupName).Message(message, userName);
        }
    }
}