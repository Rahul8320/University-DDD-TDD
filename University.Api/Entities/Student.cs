using University.Api.Models;

namespace University.Api.Entities;

public class Student
{
    public Guid Id { get; init; }
    public required string Name { get; init; }

    public static Student Register(RegisterStudentRequest request)
    {
        return new Student { Id = Guid.NewGuid(), Name = request.Name };
    }
}
