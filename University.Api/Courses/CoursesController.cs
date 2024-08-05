using Microsoft.AspNetCore.Mvc;

namespace University.Api.Courses;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    [HttpPost]
    public IActionResult IncludeInCatalog()
    {
        var course = Course.IncludeInCatalog();

        return Created($"/api/courses/{course.Id}", course);
    }
}
