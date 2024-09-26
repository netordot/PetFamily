namespace PetFamily.Domain.Shared;

public class Result
{
    public string? Error { get; set; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public Result(bool isSuccess, string? error)
    {
        if (isSuccess && error is not null)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error is null)
        {
            throw new InvalidOperationException();
        }
        
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result Success() => new Result(true, null);
    public static Result Failure(string error) => new Result(false, error);
    public static implicit operator Result(string error) => new( false, error);

    
}

public class Result<T> : Result
{
    private readonly T _value;
    
    public T Value => IsSuccess 
        ? _value 
        : throw new InvalidOperationException("The value of a failure can not be accessed");

    public Result(T value, bool isSuccess, string? error) : base(isSuccess, error)
    {
        _value = value;
    }
    
    public static Result<T> Success(T value) => new Result<T>(value, true, null);
    public static Result<T> Failure(string error) => new Result<T>(default!, false, error);
    
    public static implicit operator Result<T>(T value) => new(value, true, null);
    
    public static implicit operator Result<T>(string error) => new(default!, false, error);
    
}