using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathCase.Infrastructure.Services.Interfaces;
using PathCase.Models.ChatRooms;

namespace PathCase.Domain.Queries
{
    public class GetRoomMessagesQuery : IRequest<GetAllRoomMessagesResponseModel>
    {
        public string RoomName { get; set; }
        public string UserName { get; set; }
    }
    public class GetRoomMessagesQueryHandler : IRequestHandler<GetRoomMessagesQuery, GetAllRoomMessagesResponseModel>
    {
        private readonly IChatRoomService _chatRoomService;

        public GetRoomMessagesQueryHandler(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        public Task<GetAllRoomMessagesResponseModel> Handle(GetRoomMessagesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetAllRoomMessagesResponseModel()
            {
                Messages = _chatRoomService.GetChatLogs(roomName: request.RoomName)
                    .Select(x => new ChatMessageModel()
                    {
                        Message = x.Message,
                        Sender = x.Sender,
                        Source = request.UserName == x.Sender ? "your-message" : "other-message"
                    })
            });
        }
    }

}