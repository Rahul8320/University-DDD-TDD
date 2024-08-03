using Microsoft.AspNetCore.Mvc;
using University.Api.Entities;
using University.Api.Models;

namespace University.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RegisterStudentRequest request)
    {
        var student = Student.Register(request);

        return Created($"/api/student/{student.Id}", student);
    }
}