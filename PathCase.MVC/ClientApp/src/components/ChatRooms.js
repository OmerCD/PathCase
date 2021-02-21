import React, { useEffect, useState } from 'react';
import { Redirect, withRouter } from 'react-router-dom';
import './ChatRooms.css';
import AfterRender from './AfterRender';
import ChatMessage from './ChatMessage';
import { AxiosContext } from '../contexts/AxiosContext';
import ChatRoom from './ChatRoom';
import ChatRoomService from '../services/ChatRoomService';
import { SignalRContext } from '../contexts/SignalRContext';

function ChatRooms({ location, history }) {
	const [roomMessages, setRoomMessages] = useState([]);
	const newMessageHandler = (message, userName) => {
		setRoomMessages([...roomMessages, { message: message, sender: userName, source: 'other-message' }]);
	};
	const axios = React.useContext(AxiosContext);
	const [signalRConnected, setSignalRConnected] = useState(false);
	const chatRoomService = new ChatRoomService(
		axios,
		React.useContext(SignalRContext),
		newMessageHandler,
		() => setSignalRConnected(true),
		() => setSignalRConnected(false)
	);

	const [rooms, setRooms] = useState([]);
	const [chatEnabled, setChatEnabled] = useState(false);
	const [currentRoomName, setCurrentRoomName] = useState('No Room Selected');

	const scrollToEnd = () => {
		var element = document.getElementById('message-area');
		if (element) {
			element.scrollTop = element.scrollHeight;
		}
	};
	useEffect(() => {
		scrollToEnd();
	}, [roomMessages]);
	useEffect(() => {
		axios.get('chat/rooms').then((response) => {
			setRooms(response.data.rooms);
		});
	}, []);
	if (!location.state || !location.state.userName) {
		return <Redirect to={'/'} />;
	}
	const { userName } = location.state;

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

	const sendMessage = async () => {
		const element = document.getElementById('userMessage');
		const message = element.value;
		element.value = "";
		await chatRoomService.sendMessage(currentRoomName, message);
		setRoomMessages([...roomMessages, { message: message, sender: userName, source: 'your-message' }]);
	};
	const handleUserKeyPress = e => {
		if (e.key === "Enter" && !e.shiftKey) {
			e.preventDefault();
			sendMessage(); // this won't be triggered
		  }
	}
	return (
		<div style={{ background: 'rebeccapurple' }}>
			<AfterRender action={scrollToEnd} />
			<div className={'joined-room-name'}>{currentRoomName}</div>
			<div id={'message-area'} className="message-area">
				{roomMessages.map((roomMessage) => (
					<ChatMessage key={roomMessage} {...roomMessage} />
				))}
			</div>
			<form className="chat-input" onSubmit={sendMessage}>
				<textarea id={'userMessage'} cols="80" disabled={!chatEnabled} onKeyPress={handleUserKeyPress}></textarea>
				<button disabled={!chatEnabled} type={"submit"}>
					Send
				</button>
			</form>
			<div className="chat-room-name">Chat Rooms</div>
			<div className="chat-rooms">
				{rooms.map((room) => (
					<ChatRoom
						key={room.roomName}
						disabled={!signalRConnected}
						onClick={handleChatRoomClick}
						roomName={room.roomName}
					/>
				))}
			</div>
		</div>
	);
}

export default withRouter(ChatRooms);
