using API.SignalRHub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSignalrController : ControllerBase
    {
        [HttpGet("Room")]
        public async Task<IActionResult> GetRoomMap()
        {
            return Ok(MeetingHub.Rooms);
        }

        [HttpGet("Presence")]
        public async Task<IActionResult> GetPresenceMap()
        {
            return Ok(MeetingHub.Rooms);
        }

        [HttpGet("ShareScreen")]
        public async Task<IActionResult> GetShareScreenMap()
        {
            return Ok(MeetingHub.Rooms);
        }
    }
}
