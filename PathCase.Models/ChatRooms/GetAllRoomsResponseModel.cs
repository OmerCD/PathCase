using System.Collections.Generic;

namespace PathCase.Models.ChatRooms
{
    public class GetAllRoomsResponseModel
    {
        public IEnumerable<RoomInfoResponseModel> Rooms { get; set; }
    }

    public class RoomInfoResponseModel
    {
        public string RoomName { get; set; }
    }
}