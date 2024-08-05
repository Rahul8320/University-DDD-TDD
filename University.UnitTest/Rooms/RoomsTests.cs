using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using University.Api;

namespace University.UnitTest.Rooms;

public class RoomsTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async void GivenIAmAdmin_WhenSetupARoom()
    {
        var api = new RoomsApi(_factory.CreateClient());

        var roomSetupRequest = new RoomSetupRequest
        {
            Name = Guid.NewGuid().ToString(),
            Capacity = 5
        };

        var (response, room) = await api.SetupRoom(roomSetupRequest);

        ItShouldSetupANewRoom(response);
        ItShouldAllocateANewId(room);
        ItShouldShowWhereToLocateNewRoom(response, room);
        ItShouldConfirmRoomDetails(roomSetupRequest, room);
    }

    private static void ItShouldConfirmRoomDetails(RoomSetupRequest roomSetupRequest, RoomResponse? room)
    {
        Assert.NotEqual(room?.Name, string.Empty);
        Assert.NotEqual(room?.Capacity, 0);
        Assert.Equal(roomSetupRequest.Name, room?.Name);
        Assert.Equal(roomSetupRequest.Capacity, room?.Capacity);
    }

    private static void ItShouldShowWhereToLocateNewRoom(HttpResponseMessage response, RoomResponse? room)
    {
        var location = response.Headers.Location;
        Assert.NotNull(location);
        Assert.Equal($"/api/rooms/{room?.Id}", location.ToString());
    }

    private static void ItShouldAllocateANewId(RoomResponse? room)
    {
        Assert.NotNull(room);
        Assert.NotEqual(room.Id, Guid.Empty);
    }

    private static void ItShouldSetupANewRoom(HttpResponseMessage response)
    {
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
