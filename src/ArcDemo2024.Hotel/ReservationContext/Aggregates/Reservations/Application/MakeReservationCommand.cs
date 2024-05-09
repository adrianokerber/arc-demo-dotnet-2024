using CSharpFunctionalExtensions;

namespace ArcDemo2024.Hotel.ReservationContext.Aggregates.Reservations.Application;

public sealed class MakeReservationCommand
{
    private MakeReservationCommand()
    {
        throw new NotImplementedException();
    }

    public static Result<MakeReservationCommand> Create()
    {
        // TODO: create parameters and check before start command!
        
        return new MakeReservationCommand();
    }
}