using Microsoft.AspNetCore.Mvc;
using University.Api.Data;

namespace University.Api.Rooms;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(UniversityDbContext context) : ControllerBase
{
    private readonly UniversityDbContext _context = context;

    [HttpPost]
    public async Task<ActionResult<Room>> SetupNewRoom([FromBody] RoomSetupRequest request)
    {
        var room = Room.Setup(request);

        await _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRoomDetails", new { Id = room.Id }, room);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<Room>> GetRoomDetails([FromRoute] Guid id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }
}
