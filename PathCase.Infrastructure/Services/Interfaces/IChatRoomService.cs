using System.Collections.Generic;
using PathCase.Core.Entities;

namespace PathCase.Infrastructure.Services.Interfaces
{
    public interface IChatRoomService : IScopedService
    {
        IEnumerable<ChatRoom> GetChatRooms();
        void AddChatLog(string groupName, string message,string sender);
        IEnumerable<ChatLog> GetChatLogs(string roomName);
    }
}