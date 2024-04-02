namespace ArcDemo2024.Hotel.Shared.ResultPattern;

public sealed record Error(string Code, string? Message = null);