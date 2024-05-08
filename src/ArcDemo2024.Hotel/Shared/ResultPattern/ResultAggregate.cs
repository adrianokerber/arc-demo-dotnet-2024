namespace ArcDemo2024.Hotel.Shared.ResultPattern;

public sealed class ResultAggregate<TValue, TError>
{
    public readonly IEnumerable<Result<TValue, TError>> Successes;
    public readonly IEnumerable<Result<TValue, TError>> Errors;
    
    public bool IsSuccess => !IsFailure;
    public bool IsFailure => Errors.Any();

    private ResultAggregate(params Result<TValue, TError>[] results)
    {
        Successes = results.Where(x => x.IsSuccess);
        Errors = results.Where(x => x.IsFailure);
    }

    public static ResultAggregate<TValue, TError> From(params Result<TValue, TError>[] results)
    {
        return new ResultAggregate<TValue, TError>(results);
    }
}