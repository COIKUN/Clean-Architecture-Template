namespace Application.Common.Models;

public class Result<T> : Result
{
    public T? Value { get; }
    
    private Result(bool isSuccess, T? value, string error) : base(isSuccess, error)
    {
        Value = value;
    }
    
    public static Result<T> Success(T value) => new(true, value, string.Empty);
    public static Result<T> Failure(string error) => new(false, default, error);
}