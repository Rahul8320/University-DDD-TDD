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
}
