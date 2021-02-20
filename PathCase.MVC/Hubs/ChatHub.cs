using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PathCase.MVC.Hubs.Client;

namespace PathCase.MVC.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ServerMessage($"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).ServerMessage($"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}