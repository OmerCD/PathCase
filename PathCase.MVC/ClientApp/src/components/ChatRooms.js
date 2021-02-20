import React from "react";
import { withRouter } from "react-router-dom";
import "./ChatRooms.css";
import AfterRender from "./AfterRender";
import ChatMessage from "./ChatMessage";

function ChatRooms({ location }) {
  const { userName } = location.state;
  const messages = [{ message: "", sender: "", source: "other-message" }];
  return (
    <div style={{background:"rebeccapurple"}}>
      <AfterRender
        action={() => {
          var element = document.getElementById("message-area");
          element.scrollTop = element.scrollHeight;
        }}
      />
      <div id={"message-area"} className="message-area">
        <ChatMessage message="Test" sender="Mahmut" source="other-message" />
        <ChatMessage message="Test" sender="Irfan" source="your-message" />
      </div>
      <div className="chat-input">
        <textarea cols="80"></textarea>
        <button>Send</button>
      </div>
      <div className="chat-rooms">
        <div className="chat-room">Mahmut</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
        <div className="chat-room">İrfan</div>
      </div>
    </div>
  );
}

export default withRouter(ChatRooms);
