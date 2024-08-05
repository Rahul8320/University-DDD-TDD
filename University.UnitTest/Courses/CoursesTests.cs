using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using University.Api;

namespace University.UnitTest.Courses;

public class CoursesTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task GivenIAmAnAdmin_WhenIIncludeANewCourseInTheCatalog()
    {
        var api = new CoursesApi(_factory.CreateClient());

        var response = await api.IncludeInCatalog();
        ItShouldIncludeTheCourseInTheCatalog(response);
    }

    private static void ItShouldIncludeTheCourseInTheCatalog(HttpResponseMessage response)
    {
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
