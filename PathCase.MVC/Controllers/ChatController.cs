using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PathCase.Domain.Queries;

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
    }
}