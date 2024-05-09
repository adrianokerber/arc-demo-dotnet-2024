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
    
    /// <summary>
    /// WARNING: this method is not recommended to use! Use Result directly for a specific action or ResultAggregate to join multiple results on a call chain, but avoid re-joining ResultAggregates as you might be creating a nesting issue...
    /// </summary>
    /// <param name="resultAggregates"></param>
    /// <returns></returns>
    public static ResultAggregate<TValue, TError> Combine(params ResultAggregate<TValue, TError>[] resultAggregates)
    {
        IEnumerable<Result<TValue, TError>> results = new List<Result<TValue, TError>>();
        foreach (ResultAggregate<TValue,TError> aggregate in resultAggregates)
        {
            results = results.Concat(aggregate.Successes)
                             .Concat(aggregate.Errors);
        }

        return new ResultAggregate<TValue, TError>(results.ToArray());
    }
}