using Microsoft.AspNetCore.Mvc;
using University.Api.Entities;

namespace University.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    [HttpPost]
    public IActionResult Register()
    {
        return Created("", Student.Register());
    }
}