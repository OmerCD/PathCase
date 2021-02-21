using System.Collections.Generic;

namespace PathCase.Models.ChatRooms
{

    public class GetAllRoomMessagesResponseModel
    {
        public IEnumerable<ChatMessageModel> Messages { get; set; }
    }
    public class GetAllRoomsResponseModel
    {
        public IEnumerable<RoomInfoResponseModel> Rooms { get; set; }
    }

    public class ChatMessageModel
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
    }
    public class RoomInfoResponseModel
    {
        public string RoomName { get; set; }
    }
}