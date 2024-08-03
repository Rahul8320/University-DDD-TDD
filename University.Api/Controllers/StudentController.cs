using Microsoft.AspNetCore.Mvc;
using University.Api.Data;
using University.Api.Entities;
using University.Api.Models;

namespace University.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(UniversityDbContext context) : ControllerBase
{
    private readonly UniversityDbContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterStudentRequest request)
    {
        var student = Student.Register(request);
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return Created($"/api/student/{student.Id}", student);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetStudentDetails([FromRoute] Guid id)
    {
        var student = await _context.Students.FindAsync(id);

        return Ok(student);
    }
}