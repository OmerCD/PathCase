import React from 'react';

function ChatRoom({ onClick, roomName,disabled, selected }) {
	return (
		<div disabled={disabled} onClick={() => onClick(roomName)} className={`chat-room ${selected ? 'selected' : ''}`}>
			{roomName}
		</div>
	);
}

export default ChatRoom;
