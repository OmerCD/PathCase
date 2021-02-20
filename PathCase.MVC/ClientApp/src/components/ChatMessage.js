import React from "react";

function ChatMessage({ message, sender, source }) {
  return (
    <div className={`${source} message`}>
        {message}
        <span className="sender">{sender}</span>
    </div>
  );
}

export default ChatMessage;
