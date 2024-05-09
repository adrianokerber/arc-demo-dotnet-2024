using CSharpFunctionalExtensions;

namespace ArcDemo2024.Hotel.ReservationContext.Aggregates.Reservations.Application;

public sealed class MakeReservationCommandHandler
{
    public async Task<Result> Handle(MakeReservationCommand command, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}