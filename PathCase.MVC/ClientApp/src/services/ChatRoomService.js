class ChatRoomService {
  constructor(signalRConnection, onMessage) {
    signalRConnection.on("Message", onMessage);
    this.connection = signalRConnection;
  }
 
  async joinRoom(roomName) {
    this.connection.invoke("JoinGroup", roomName);
    return true;
  }
  async sendMessage(roomName, message){
      await this.connection.invoke("SendMessage", roomName, message);
  }
  async getRoomMessages(roomName) {
    return [
      { message: "Hello", sender: "Visca", source: "other-message" },
      { message: "Hello", sender: "İrfan", source: "other-message" },
      { message: "Hello", sender: "You", source: "other-message" },
      { message: "Hello", sender: "Mahmut", source: "your-message" },
    ];
  }
}

export default ChatRoomService;
