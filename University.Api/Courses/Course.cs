namespace University.Api.Courses;

public class Course
{
    public Guid Id { get; init; }
    public required string Name { get; set; }

    public static Course IncludeInCatalog(IncludeCourseInCatalogRequest request)
    {
        return new Course { Id = Guid.NewGuid(), Name = request.Name };
    }
}
