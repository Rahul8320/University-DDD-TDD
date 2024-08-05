using System.Net.Http.Json;

namespace University.UnitTest.Courses;

public class CoursesApi(HttpClient client)
{
    private readonly HttpClient _client = client;

    public async Task<(HttpResponseMessage, CourseResponse?)> IncludeInCatalog(IncludeCourseInCatalogRequest request)
    {
        var response = await _client.PostAsync("/api/courses", JsonContent.Create(request));
        var course = await response.Content.ReadFromJsonAsync<CourseResponse>();

        return (response, course);
    }
}
