using System.Collections.Generic;
using PathCase.Core.Entities;
using PathCase.Infrastructure.Repositories.Interfaces;
using PathCase.Infrastructure.Services.Interfaces;

namespace PathCase.Infrastructure.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IChatRoomReposıtory _roomReposıtory;

        public ChatRoomService(IChatRoomReposıtory roomReposıtory)
        {
            _roomReposıtory = roomReposıtory;
        }

        public IEnumerable<ChatRoom> GetChatRooms()
        {
            return _roomReposıtory.GetAll();
        }

        public void AddChatLog(string groupName, string message, string sender)
        {
            _roomReposıtory.AddChatLog(groupName, new ChatLog()
            {
                Message = message,
                Sender = sender
            });
        }

        public IEnumerable<ChatLog> GetChatLogs(string roomName)
        {
            return _roomReposıtory.GetChatLog(roomName);
        }
    }
}