using Microsoft.AspNetCore.Mvc;
using University.Api.Data;

namespace University.Api.Courses;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(UniversityDbContext context) : ControllerBase
{
    private readonly UniversityDbContext _context = context;

    [HttpPost]
    public async Task<ActionResult<Course>> IncludeInCatalog([FromBody] IncludeCourseInCatalogRequest request)
    {
        var course = Course.IncludeInCatalog(request);

        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCourseDetails", new { Id = course.Id }, course);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<Course>> GetCourseDetails([FromRoute] Guid id)
    {
        var course = await _context.Courses.FindAsync(id);

        return Ok(course);
    }
}
