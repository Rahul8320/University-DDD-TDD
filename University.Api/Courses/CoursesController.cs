using Microsoft.AspNetCore.Mvc;

namespace University.Api.Courses;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    [HttpPost]
    public IActionResult IncludeInCatalog()
    {
        return Created("", null);
    }
}
