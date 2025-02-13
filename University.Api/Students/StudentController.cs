using Microsoft.AspNetCore.Mvc;
using University.Api.Data;

namespace University.Api.Students;

[ApiController]
[Route("api/[controller]")]
public class StudentController(UniversityDbContext context) : ControllerBase
{
    private readonly UniversityDbContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterStudentRequest request)
    {
        var student = Student.Register(request);
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStudentDetails", new { student.Id }, student);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<Student>> GetStudentDetails([FromRoute] Guid id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        return Ok(student);
    }
}