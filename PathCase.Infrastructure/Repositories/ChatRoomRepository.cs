using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MongoDB.Driver;
using PathCase.Core.Entities;
using PathCase.Infrastructure.Repositories.Interfaces;

namespace PathCase.Infrastructure.Repositories
{
    public class ChatRoomRepository : IChatRoomReposıtory
    {
        private readonly IMongoDatabase _mongoDatabase;
        private IMongoCollection<ChatRoom> Set => _mongoDatabase.GetCollection<ChatRoom>(nameof(ChatRoom));

        public ChatRoomRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IEnumerable<ChatRoom> GetAll()
        {
            return Set.AsQueryable().ToList();
        }

        public void AddChatLog(string roomName, ChatLog chatLog)
        {
            var filter = Builders<ChatRoom>.Filter.Eq("Name", roomName);

            var room = Set.Find(filter).First(); //Set.AsQueryable().FirstOrDefault(x => string.Equals(x.Name, roomName, StringComparison.InvariantCultureIgnoreCase));
            room.ChatLogs ??= new List<ChatLog>();
            room.ChatLogs.Add(chatLog);
            var update = Builders<ChatRoom>.Update.Set("ChatLogs", room.ChatLogs);
            Set.UpdateOne(filter, update);
        }

        public IList<ChatLog> GetChatLog(string roomName)
        {
            var filter = Builders<ChatRoom>.Filter.Eq("Name", roomName);
            var room = Set.Find(filter).Single();
            return room?.ChatLogs ?? new List<ChatLog>();
        }
    }
}