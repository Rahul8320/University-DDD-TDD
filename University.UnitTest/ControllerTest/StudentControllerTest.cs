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
        var api = CreateStudentApi();

        var request = new RegisterStudentRequest { Name = "Test Student" };

        var (response, student) = await api.RegisterStudent(request);

        var (newStudentResponse, newStudent) = await api.GetStudent(response.Headers.Location);

        ItShouldRegisterTheStudent(response);
        ItShouldAllocateANewId(student);
        ItShouldShowWhereToLocateNewStudent(response, student);
        ItShouldConfirmTheStudentDetails(request, student);
        ItShouldFindTheNewStudent(newStudentResponse);
        ItShouldConfirmTheStudentDetails(request, newStudent);
    }

    [Theory]
    [InlineData("Test Student")]
    [InlineData("Another Student")]
    [InlineData("Old Student")]
    public async Task GivenIHaveRegister_WhenCheckMyDetails(string studentName)
    {
        var api = CreateStudentApi();

        var request = new RegisterStudentRequest { Name = studentName };

        var (response, _) = await api.RegisterStudent(request);

        var (newStudentResponse, newStudent) = await api.GetStudent(response.Headers.Location);

        ItShouldFindTheNewStudent(newStudentResponse);
        ItShouldConfirmTheStudentDetails(request, newStudent);
    }

    private StudentApi CreateStudentApi()
    {
        var client = _factory.CreateClient();

        return new StudentApi(client);
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
        Assert.Equal($"http://localhost/api/Student/{student?.Id}", location.ToString());
    }

    private static void ItShouldConfirmTheStudentDetails(RegisterStudentRequest request, StudentResponse? student)
    {
        Assert.NotEqual(student?.Name, string.Empty);
        Assert.Equal(request.Name, student?.Name);
    }

    private static void ItShouldFindTheNewStudent(HttpResponseMessage response)
    {
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}