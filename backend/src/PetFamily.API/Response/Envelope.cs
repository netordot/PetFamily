using PetFamily.Domain.Shared.Errors;

namespace PetFamily.API.Response;

public record Envelope
{
    public object? Result { get; }

    public string? ErrorCode { get; }

    public string? ErrorMessage { get; }

    public DateTime TimeOccured { get; }

    private Envelope(object? result, Error error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        TimeOccured = DateTime.Now;
    }


    public static Envelope Ok(object? result = null)
        => new Envelope(result, null);
    
    public static Envelope Error(Error error)
        => new Envelope(null, error);
    
    
}