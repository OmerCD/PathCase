using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PathCase.Infrastructure.Services;
using PathCase.Infrastructure.Services.Interfaces;
using PathCase.Models.ChatRooms;

namespace PathCase.Domain.Queries
{
    public class GetAllRoomsQuery : IRequest<GetAllRoomsResponseModel>
    {

    }
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, GetAllRoomsResponseModel>
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly RedisService _redisService;
        public GetAllRoomsQueryHandler(IChatRoomService chatRoomService, RedisService redisService)
        {
            _chatRoomService = chatRoomService;
            _redisService = redisService;
        }

        public Task<GetAllRoomsResponseModel> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = _redisService.GetFromJson<IEnumerable<RoomInfoResponseModel>>("rooms");
            if (rooms == null)
            {
                var chatRooms = _chatRoomService.GetChatRooms();
                rooms = chatRooms.Select(x => new RoomInfoResponseModel()
                {
                    RoomName = x.Name
                });
                _redisService.SetAsJson("rooms", rooms);
            }
            return Task.FromResult(new GetAllRoomsResponseModel()
            {
                Rooms = rooms
            });
        }
    }


}