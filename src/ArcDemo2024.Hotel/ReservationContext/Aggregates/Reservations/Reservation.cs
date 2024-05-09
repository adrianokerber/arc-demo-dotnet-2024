using ArcDemo2024.Hotel.Shared;
using ArcDemo2024.Hotel.Shared.ResultPattern.CSharpFuncionalExtensions;
using CSharpFunctionalExtensions;

namespace ArcDemo2024.Hotel.ReservationContext.Aggregates.Reservations;

public sealed class Reservation : Entity<Guid>
{
    private Room Room { get; init; }
    private Stay Stay { get; init; }
    private Guest Guest { get; init; }

    private Reservation(Room room, Stay stay, Guest guest)
    {
        Room = room;
        Stay = stay;
        Guest = guest;
    }

    public static ResultAggregate<Reservation> Create(Room room, Stay stay, Guest who, DateTimeOffset when)
    {
        // TODO: review concept of staying with start date and final date...
        var result = ResultAggregate.Combine(ResultAggregate.FailureIf(room == null, "Room", "Could create a Reservation because `Room` was not defined"),
                                             ResultAggregate.FailureIf(when == null, "When", "Could create a Reservation because `When` was not defined"),
                                             ResultAggregate.FailureIf(stay == null || (stay.CheckIn >= when && stay.CheckOut <= when), "When", "Could create a Reservation because `Stay` was not defined"),
                                             ResultAggregate.FailureIf(who == null, "Who", "Could create a Reservation because `Who` was not defined"));

        if (result.IsFailure)
            return result.ConvertFailure<Reservation>();
        
        return new Reservation(room, stay, who);
    }
}

public record struct Stay(DateTimeOffset CheckIn, DateTimeOffset CheckOut);

public sealed class Room
{
    public string Name { get; init; }
    public RoomType Type { get; init; }
}

public enum RoomType
{
}

public sealed class Guest
{
    public string Cpf { get; init; }
    private string Name { get; init; }
}
