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
  const [roomMessages, setRoomMessages] = useState([]);

  const axios = React.useContext(AxiosContext);
  const signalRConnection = React.useContext(SignalRContext);
  const [signalRConnected, setSignalRConnected] = useState(
    signalRConnection.connectionState == "Connected"
  );

  const [rooms, setRooms] = useState([]);
  const [chatEnabled, setChatEnabled] = useState(false);
  const [currentRoomName, setCurrentRoomName] = useState("No Room Selected");

  const scrollToEnd = () => {
    var element = document.getElementById("message-area");
    if (element) {
      element.scrollTop = element.scrollHeight;
    }
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
  const newMessageHandler = (message, userName) => {
    setRoomMessages([
      ...roomMessages,
      { message: message, sender: userName, source: "other-message" },
    ]);
  };
  const serverMessageHandler = (message) => {
    setRoomMessages([
      ...roomMessages,
      { message: message, sender: "Server", source: "server-message" },
    ]);
  };
  const chatRoomService = new ChatRoomService(
    axios,
    signalRConnection,
    newMessageHandler,
    serverMessageHandler,
    () => setSignalRConnected(true),
    () => setSignalRConnected(false)
  );

  const handleChatRoomClick = async (roomName) => {
    if (signalRConnected) {
      if (await chatRoomService.joinRoom(roomName)) {
        const messages = await chatRoomService.getRoomMessages(roomName);
        setRoomMessages(messages);
        setCurrentRoomName(roomName);
        setChatEnabled(true);
      }
    }
  };

  const sendMessage = async (e) => {
    e.preventDefault();
    const element = document.getElementById("userMessage");
    const message = element.value;
    if (!message || message.length == 0) {
      return;
    }
    element.value = "";
    await chatRoomService.sendMessage(currentRoomName, message);
    setRoomMessages([
      ...roomMessages,
      { message: message, sender: userName, source: "your-message" },
    ]);
  };
  const handleUserKeyPress = (e) => {
    if (e.key === "Enter" && !e.shiftKey) {
      sendMessage(e);
    }
  };
  return (
    <div style={{ background: "rebeccapurple" }}>
      <AfterRender action={scrollToEnd} />
      <div className={"joined-room-name"}>{currentRoomName}</div>
      <div id={"message-area"} className="message-area">
        {roomMessages.map((roomMessage, i) => (
          <ChatMessage key={i} {...roomMessage} />
        ))}
      </div>
      <form className="chat-input" onSubmit={sendMessage}>
        <textarea
          id={"userMessage"}
          cols="80"
          disabled={!chatEnabled}
          onKeyPress={handleUserKeyPress}
        ></textarea>
        <button disabled={!chatEnabled} type={"submit"}>
          Send
        </button>
      </form>
      <div className="chat-room-name">Chat Rooms</div>
      <div className="chat-rooms">
        {rooms.map((room) => {
			return (
				<ChatRoom
					key={room.roomName}
					disabled={!signalRConnected}
					selected={currentRoomName == room.roomName}
					onClick={handleChatRoomClick}
					roomName={room.roomName} />
			);
		})}
      </div>
    </div>
  );
}

export default withRouter(ChatRooms);
