using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathCase.Infrastructure.Services.Interfaces;
using PathCase.Models.ChatRooms;

namespace PathCase.Domain.Queries
{
    public class GetAllRoomsQuery : IRequest<GetAllRoomsResponseModel>
    {
        
    }
    public class  GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, GetAllRoomsResponseModel>
    {
        private readonly IChatRoomService _chatRoomService;

        public GetAllRoomsQueryHandler(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        public Task<GetAllRoomsResponseModel> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var chatRooms = _chatRoomService.GetChatRooms();
            return Task.FromResult(new GetAllRoomsResponseModel()
            {
                Rooms =  chatRooms.Select(x=> new RoomInfoResponseModel()
                {
                    RoomName = x.Name
                })
            });
        }
    }
}