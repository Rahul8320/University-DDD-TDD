using System.Net.Http.Json;
using University.UnitTest.Models;

namespace University.UnitTest.ControllerTest;

public class StudentApi(HttpClient client)
{
    private readonly HttpClient _client = client;

    public async Task<(HttpResponseMessage, StudentResponse?)> RegisterStudent(RegisterStudentRequest request)
    {
        var response = await _client.PostAsync("/api/student", JsonContent.Create(request));
        var student = await response.Content.ReadFromJsonAsync<StudentResponse>();

        return (response, student);
    }

    public async Task<(HttpResponseMessage, StudentResponse?)> GetStudent(Uri? studentLocation)
    {
        var response = await _client.GetAsync(studentLocation);
        var student = await response.Content.ReadFromJsonAsync<StudentResponse>();

        return (response, student);
    }
}
