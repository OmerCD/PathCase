using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using PathCase.Core.ValueObjects;

namespace PathCase.Infrastructure.Services
{
    public class ChatGroupManager
    {
        public ConcurrentDictionary<string, RoomInfo> Rooms { get; set; }

        public ChatGroupManager()
        {
            Rooms = new ConcurrentDictionary<string, RoomInfo>();
        }

        public void AddUser(string roomName, UserInfo userInfo)
        {
            var room = new RoomInfo(roomName);
            room.Users.Add(userInfo);
            Rooms.AddOrUpdate(roomName,room , (_, roomInfo) =>
            {
                roomInfo.Users.Add(userInfo);
                return roomInfo;
            });
        }

        public RoomInfo GetRoom(string roomName)
        {
            if (Rooms.TryGetValue(roomName, out var roomInfo))
            {
                return roomInfo;
            }

            return null;
        }

        public void RemoveUser(string connectionId)
        {
            foreach (var keyValuePair in Rooms)
            {
                keyValuePair.Value.RemoveUser(connectionId);
            }
        }
    }
}