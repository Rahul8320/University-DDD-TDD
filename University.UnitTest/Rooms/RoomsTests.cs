using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using University.Api;

namespace University.UnitTest.Rooms;

public class RoomsTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task GivenIAmAdmin_WhenSetupARoom()
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

    [Theory]
    [InlineData("Test Room", 5)]
    [InlineData("Another Test Room", 15)]
    [InlineData("Help Desk room", 10)]
    public async Task GivenIAmSetupRoom_WhenICheckItsDetails(string name, int capacity)
    {
        var api = new RoomsApi(_factory.CreateClient());

        var roomSetupRequest = new RoomSetupRequest
        {
            Name = name,
            Capacity = capacity
        };

        var (response, _) = await api.SetupRoom(roomSetupRequest);

        var (newRoomResponse, newRoom) = await api.GetRoom(response.Headers.Location);

        ItShouldFindTheNewRoom(newRoomResponse);
        ItShouldConfirmRoomDetails(roomSetupRequest, newRoom);
    }

    [Fact]
    public async Task GivenIHaveTheWrongId_WhenICheckTheRoom()
    {
        var api = new RoomsApi(_factory.CreateClient());

        var wrongId = Guid.NewGuid();

        var (response, _) = await api.GetRoom(wrongId);

        ItShouldNotFindTheRoom(response);
    }

    private static void ItShouldNotFindTheRoom(HttpResponseMessage response)
    {
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static void ItShouldFindTheNewRoom(HttpResponseMessage newRoomResponse)
    {
        Assert.Equal(HttpStatusCode.OK, newRoomResponse.StatusCode);
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
        var uri = RoomsApi.UriForRoomId(room?.Id);

        Assert.NotNull(location);
        Assert.Equal(uri.ToString(), location.ToString());
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
