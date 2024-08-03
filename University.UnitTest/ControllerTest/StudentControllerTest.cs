using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using University.Api;
using University.UnitTest.Models;

namespace University.UnitTest.ControllerTest;

public class StudentControllerTest(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task GivenIAmAStudent_WhenIRegister()
    {
        var client = _factory.CreateClient();
        var request = new RegisterStudentRequest { Name = Guid.NewGuid().ToString() };

        var response = await client.PostAsync("/api/student", JsonContent.Create(request));

        var student = await response.Content.ReadFromJsonAsync<StudentResponse>();

        ItShouldRegisterTheStudent(response);
        ItShouldAllocateANewId(student);
        ItShouldShowWhereToLocateNewStudent(response, student);
        ItShouldConfirmStudentDetails(request, student);
    }

    private static void ItShouldRegisterTheStudent(HttpResponseMessage response)
    {
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    private static void ItShouldAllocateANewId(StudentResponse? student)
    {
        Assert.NotNull(student);
        Assert.NotEqual(Guid.Empty, student.Id);
    }

    private static void ItShouldShowWhereToLocateNewStudent(HttpResponseMessage response, StudentResponse? student)
    {
        var location = response.Headers.Location;

        Assert.NotNull(location);
        Assert.Equal($"/api/student/{student?.Id}", location.ToString());
    }

    private static void ItShouldConfirmStudentDetails(RegisterStudentRequest request, StudentResponse? student)
    {
        Assert.NotEqual(student?.Name, string.Empty);
        Assert.Equal(request.Name, student?.Name);
    }
}