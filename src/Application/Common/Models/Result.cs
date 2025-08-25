namespace Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }
    
    protected Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error) => new(false, error);
}

