using System.Collections.Generic;
using System.Linq;

namespace PathCase.Core.ValueObjects
{
    public class RoomInfo
    {
        public RoomInfo(string roomName)
        {
            RoomName = roomName;
        }

        public IList<UserInfo> Users { get; set; } = new List<UserInfo>();
        public string RoomName { get; set; }
        public override int GetHashCode()
        {
            return RoomName.GetHashCode();
        }

        public void RemoveUser(string connectionId)
        {
            Users.Remove(Users.FirstOrDefault(x => x.ConnectionId == connectionId));
        }
    }
}