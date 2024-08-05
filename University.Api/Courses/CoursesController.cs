using Microsoft.AspNetCore.Mvc;

namespace University.Api.Courses;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    [HttpPost]
    public IActionResult IncludeInCatalog([FromBody] IncludeCourseInCatalogRequest request)
    {
        var course = Course.IncludeInCatalog(request);

        return Created($"/api/courses/{course.Id}", course);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetCourseDetails([FromRoute] Guid id)
    {
        var course = new Course { Id = id, Name = "Test Course" };

        return Ok(course);
    }
}
