using ArcDemo2024.Hotel.ReservationContext.Aggregates.Reservations.Application;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace ArcDemo2024.Hotel.ReservationContext.Aggregates.Reservations.Endpoints;

public class ReservationPostEndpoint : Endpoint<Request, Response>
{
    private readonly MakeReservationCommandHandler _handler;
    
    public ReservationPostEndpoint(MakeReservationCommandHandler handler)
    {
        _handler = handler;
    }
    
    public async Task<IResult> HandleAsync(Request req, CancellationToken ct)
    {
        var command = MakeReservationCommand.Create();
        if (command.IsFailure)
            return Results.Problem(command.Error, statusCode: 400);
        var result = await _handler.Handle(command.Value, ct);
        if (result.IsFailure)
            return Results.Problem(command.Error, statusCode: 400);
        return Results.Ok();
    }
}

public record struct Request();

public record struct Response();