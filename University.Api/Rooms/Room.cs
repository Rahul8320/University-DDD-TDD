namespace University.Api.Rooms;

public class Room
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required int Capacity { get; init; }

    public static Room Setup(RoomSetupRequest request)
    {
        return new Room
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Capacity = request.Capacity
        };
    }
}
