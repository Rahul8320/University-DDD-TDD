namespace University.Api.Entities;

public class Student
{
    public Guid Id { get; set; }

    public static Student Register()
    {
        return new Student { Id = Guid.NewGuid() };
    }
}
