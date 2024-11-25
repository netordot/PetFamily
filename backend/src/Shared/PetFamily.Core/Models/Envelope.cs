using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Core.Models;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

public record Envelope
{
    public object? Result { get; }

    public ErrorList? Errors { get; }

    public DateTime TimeOccured { get; }

    private Envelope(object? result, ErrorList errors)
    {
        Result = result;
        Errors = errors;
        TimeOccured = DateTime.Now;
    }


    public static Envelope Ok(object? result = null)
        => new Envelope(result, null);

    public static Envelope Error(ErrorList errors)
        => new Envelope(null, errors);
}