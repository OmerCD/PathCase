using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathCase.Domain.Queries;
using PathCase.Infrastructure.Services;
using PathCase.MVC.Extensions;

namespace PathCase.MVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms()
        {
            var query = new GetAllRoomsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("roomMessages")]
        public async Task<IActionResult> GetRoomMessages(string roomName)
        {
            var name = HttpContext.User.GetName();
            var query = new GetRoomMessagesQuery()
            {
                RoomName = roomName,
                UserName = name
            };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}