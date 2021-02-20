import React, { useEffect, useState } from "react";
import { Redirect, withRouter } from "react-router-dom";
import "./ChatRooms.css";
import AfterRender from "./AfterRender";
import ChatMessage from "./ChatMessage";
import { AxiosContext } from "../contexts/AxiosContext";
import ChatRoom from "./ChatRoom";
import ChatRoomService from "../services/ChatRoomService";
import { SignalRContext } from "../contexts/SignalRContext";

function ChatRooms({ location, history }) {
  const chatRoomService = new ChatRoomService(React.useContext(SignalRContext), item => console.log(item));
  const axios = React.useContext(AxiosContext);
  const [rooms, setRooms] = useState([]);
  const [chatEnabled, setChatEnabled] = useState(false);
  const [currentRoomName, setCurrentRoomName] = useState("No Room Selected");
  const [roomMessages, setRoomMessages] = useState([]);
  
  const scrollToEnd = () => {
    var element = document.getElementById("message-area");
    element.scrollTop = element.scrollHeight;
  };
  useEffect(() => {
    scrollToEnd();
  }, [roomMessages]);
  useEffect(() => {
    axios.get("chat/rooms").then((response) => {
      setRooms(response.data.rooms);
    });
  }, []);
  if (!location.state || !location.state.userName) {
    return <Redirect to={"/"} />;
  }
  const { userName } = location.state;


  const handleChatRoomClick = async (roomName) => {
    if (await chatRoomService.joinRoom(roomName)) {
      const messages = await chatRoomService.getRoomMessages(roomName);
      setRoomMessages(messages);
      setCurrentRoomName(roomName);
      setChatEnabled(true);
    }
  };

  const sendMessage = async () => {
    const message = document.getElementById("userMessage").value;
    await chatRoomService.sendMessage(currentRoomName, message);
    setRoomMessages([
      ...roomMessages,
      { message: message, sender: userName, source: "your-message" },
    ]);
  };


  return (
    <div style={{ background: "rebeccapurple" }}>
      <AfterRender action={scrollToEnd} />
      <div className={"joined-room-name"}>{currentRoomName}</div>
      <div id={"message-area"} className="message-area">
        {roomMessages.map((roomMessage) => (
          <ChatMessage {...roomMessage} />
        ))}
      </div>
      <div className="chat-input">
        <textarea
          id={"userMessage"}
          cols="80"
          disabled={!chatEnabled}
        ></textarea>
        <button disabled={!chatEnabled} onClick={sendMessage}>
          Send
        </button>
      </div>
      <div className="chat-room-name">Chat Rooms</div>
      <div className="chat-rooms">
        {rooms.map((room) => (
          <ChatRoom onClick={handleChatRoomClick} roomName={room.roomName} />
        ))}
      </div>
    </div>
  );
}

export default withRouter(ChatRooms);
