import React from "react";

function ChatRoom({onClick, roomName}){
    return(
        <div onClick={() => onClick(roomName)} className="chat-room">{roomName}</div>
    )
}

export default ChatRoom;