namespace University.Api.Courses;

public class Course
{
    public Guid Id { get; init; }

    public static Course IncludeInCatalog()
    {
        return new Course { Id = Guid.NewGuid() };
    }
}
