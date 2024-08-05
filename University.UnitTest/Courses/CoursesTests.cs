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

        var includeCourseInCatalogRequest = new IncludeCourseInCatalogRequest
        {
            Name = Guid.NewGuid().ToString()
        };

        var (response, course) = await api.IncludeInCatalog(includeCourseInCatalogRequest);

        ItShouldIncludeTheCourseInTheCatalog(response);
        ItShouldAllocateANewId(course);
        ItShouldShowWhereToLocateNewCourse(response, course);
        ItShouldConfirmCourseDetails(includeCourseInCatalogRequest, course);
    }

    private static void ItShouldConfirmCourseDetails(
        IncludeCourseInCatalogRequest includeCourseInCatalogRequest,
        CourseResponse? course)
    {
        Assert.NotEqual(string.Empty, course?.Name);
        Assert.Equal(includeCourseInCatalogRequest.Name, course?.Name);
    }

    private static void ItShouldShowWhereToLocateNewCourse(HttpResponseMessage response, CourseResponse? course)
    {
        var location = response.Headers.Location;
        var uri = $"/api/courses/{course?.Id}";

        Assert.NotNull(location);
        Assert.Equal(uri.ToString(), location.ToString());
    }

    private static void ItShouldAllocateANewId(CourseResponse? course)
    {
        Assert.NotNull(course);
        Assert.NotEqual(Guid.Empty, course.Id);
    }

    private static void ItShouldIncludeTheCourseInTheCatalog(HttpResponseMessage response)
    {
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Theory]
    [InlineData("Test Course")]
    public async Task GivenIHaveIncludedACourse_WhenICheckTheCourseDetails(string courseName)
    {
        var api = new CoursesApi(_factory.CreateClient());

        var request = new IncludeCourseInCatalogRequest { Name = courseName };

        var (response, _) = await api.IncludeInCatalog(request);

        var (newCourseResponse, newCourse) = await api.GetCourse(response.Headers.Location);

        ItShouldFindTheNewCourse(newCourseResponse);
        ItShouldConfirmCourseDetails(request, newCourse);
    }

    private static void ItShouldFindTheNewCourse(HttpResponseMessage newCourseResponse)
    {
        Assert.Equal(HttpStatusCode.OK, newCourseResponse.StatusCode);
    }
}
