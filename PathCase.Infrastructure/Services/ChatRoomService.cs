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
    }
}