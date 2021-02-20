using System.Collections.Generic;
using PathCase.Core.Entities;

namespace PathCase.Infrastructure.Repositories.Interfaces
{
    public interface IChatRoomReposıtory : IRepository<ChatRoom>
    {
        void AddChatLog(string roomName, ChatLog chatLog);
        IList<ChatLog> GetChatLog(string roomName);
    }
}