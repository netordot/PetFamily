namespace PetFamily.Domain.Shared.Errors;

public record Error
{
    public string Code { get;}
    public string Message { get;}
    public ErorType Type { get;}

    private Error(string code, string message, ErorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }
    
    
    public static Error Validation(string code, string message) => new Error(code, message, ErorType.Validation);
    public static Error NotFound(string code, string message) => new Error(code, message, ErorType.NotFound);
    public static Error Failure(string code, string message) => new Error(code, message, ErorType.Failure);
    public static Error Conflict(string code, string message) => new Error(code, message, ErorType.Conflict);
    
}



public enum ErorType
{
    Validation,
    NotFound,
    Failure,
    Conflict,
}