namespace ArcDemo2024.Hotel.Shared.ResultPattern;

public sealed class ResultAggregate<TValue, TError>
{
    public readonly IEnumerable<Result<TValue, TError>> Successes;
    public readonly IEnumerable<Result<TValue, TError>> Errors;
    
    public bool IsFailure => Errors.Any();
    public bool IsSuccess => !IsFailure;

    private ResultAggregate(params Result<TValue, TError>[] results)
    {
        Successes = results.TakeWhile(x => x.IsSuccess);
        Errors = results.TakeWhile(x => x.IsFailure);
    }

    public static ResultAggregate<TValue, TError> From(params Result<TValue, TError>[] results)
    {
        return new ResultAggregate<TValue, TError>(results);
    }
}