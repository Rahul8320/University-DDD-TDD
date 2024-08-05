using System.Net.Http.Json;

namespace University.UnitTest.Rooms;

public class RoomsApi(HttpClient client)
{
    private readonly HttpClient _client = client;

    public async Task<(HttpResponseMessage, RoomResponse?)> SetupRoom(RoomSetupRequest request)
    {
        var response = await _client.PostAsync("/api/rooms", JsonContent.Create(request));
        var room = await response.Content.ReadFromJsonAsync<RoomResponse>();

        return (response, room);
    }

    public async Task<(HttpResponseMessage, RoomResponse?)> GetRoom(Uri? roomLocation)
    {
        var response = await _client.GetAsync(roomLocation);
        var room = await response.Content.ReadFromJsonAsync<RoomResponse>();

        return (response, room);
    }

    public static Uri UriForRoomId(Guid? roomId)
    {
        return new Uri($"http://localhost/rooms/{roomId}");
    }

    public async Task<(HttpResponseMessage, RoomResponse?)> GetRoom(Guid id)
       => await GetRoom(UriForRoomId(id));
}
