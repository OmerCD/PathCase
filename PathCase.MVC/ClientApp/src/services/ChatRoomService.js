class ChatRoomService {
	constructor(axiosService, signalRConnection, onMessage, onConnected, onClosed) {
		signalRConnection.on('Message', onMessage);
		signalRConnection.onclose(onClosed);
		if (signalRConnection.connectionState != 'Connected') {
			signalRConnection
				.start()
				.then(onConnected)
				.catch((x) => console.log(x));
		}
		this.connection = signalRConnection;
		this.axios = axiosService;
	}

	async joinRoom(roomName) {
    debugger;
		if (this.connection.connectionState == 'Connected') {
			this.connection.invoke('JoinGroup', roomName);
		}
		return true;
	}
	async sendMessage(roomName, message) {
		if (this.connection.connectionState == 'Connected') {
			await this.connection.invoke('SendMessage', roomName, message);
		}
	}
	async getRoomMessages(roomName) {
		return (await this.axios.get('Chat/roomMessages', { params: { roomName } })).data.messages;
	}
}

export default ChatRoomService;
