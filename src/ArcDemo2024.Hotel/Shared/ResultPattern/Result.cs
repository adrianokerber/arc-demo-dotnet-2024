namespace ArcDemo2024.Hotel.Shared.ResultPattern;

public sealed class Result<TValue, TError>
{
    public readonly TValue Value;
    public readonly TError Error;
    
    private readonly bool _isSuccess;
    
    public bool IsSuccess => _isSuccess;
    public bool IsFailure => !_isSuccess;

    private Result(TValue value)
    {
        _isSuccess = true;
        Value = value;
        Error = default;
    }
    private Result(TError error)
    {
        _isSuccess = false;
        Value = default;
        Error = error;
    }
    
    public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value); // Success path
    public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error); // Failure path
    
    public static Result<TValue, TError> Success(TValue value) => new Result<TValue, TError>(value);
    public static Result<TValue, TError> Failure(TError error) => new Result<TValue, TError>(error);

    // This method could be an extension...
    public Result<TValue, TError> Match(Func<TValue, Result<TValue, TError>> success,
                                        Func<TError, Result<TValue, TError>> failure) 
        => _isSuccess ? success(Value) : failure(Error);
}