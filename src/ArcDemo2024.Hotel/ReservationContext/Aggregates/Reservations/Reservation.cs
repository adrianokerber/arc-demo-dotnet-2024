using ArcDemo2024.Hotel.Shared;
using ArcDemo2024.Hotel.Shared.ResultPattern.Original;
using CSharpFunctionalExtensions;

namespace ArcDemo2024.Hotel.ReservationContext.Aggregates.Reservations;

public sealed class Reservation : Entity<Guid>
{
    private Room Room { get; init; }
    private DateTimeOffset Date { get; init; }
    private Guest Guest { get; init; }

    private Reservation(Room room, DateTimeOffset date, Guest guest)
    {
        Room = room;
        Date = date;
        Guest = guest;
    }

    public static ResultAggregate<Reservation> Create(Room room, DateTimeOffset when, Guest who)
    {
        // TODO: review concept of staying with start date and final date...
        var result = ResultAggregate.Combine(ResultAggregate.FailureIf(room == null, "Room", "Could create a Reservation because the `room` was not defined"),
                                             ResultAggregate.FailureIf(when == null, "When", "Could create a Reservation because the `who` was not defined"),
                                             ResultAggregate.FailureIf(who == null, "Who", "Could create a Reservation because the `who` was not defined"));

        if (result.IsFailure)
            return result.ConvertFailure<Reservation>();
        
        return new Reservation(room, when, who);
    }
}

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
