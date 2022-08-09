
namespace Application.Core;

public class Result<T>
{

    public bool IsSuccess { get; set; }

    public T? Value { get; set; }

    public string Error { get; set; } = string.Empty;    
}


public static class Result 
{

    public static Result<T> Success<T>(T value)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Value = value
        };
    }

    public static Result<T> Failure<T>(string error)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = error            
        };
    }
}