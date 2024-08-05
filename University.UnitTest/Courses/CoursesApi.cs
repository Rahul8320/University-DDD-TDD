namespace University.UnitTest.Courses;

public class CoursesApi(HttpClient client)
{
    private readonly HttpClient _client = client;

    public async Task<HttpResponseMessage> IncludeInCatalog()
    {
        var response = await _client.PostAsync("/api/courses", null);

        return response;
    }
}
