using Microsoft.AspNetCore.Mvc;

namespace University.Api.Rooms;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpPost]
    public IActionResult SetupNewRoom([FromBody] RoomSetupRequest request)
    {
        var room = Room.Setup(request);

        return Created($"/api/rooms/{room.Id}", room);
    }
}
