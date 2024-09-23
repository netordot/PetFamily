using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared;

public record Email
{
    private const string Pattern  = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\n";
    public string Mail { get;private set; }

    private Email(string mail)
    {
        Mail = mail;
    }

    public static Result<Email> Create(string mail)
    {
        if (!Regex.IsMatch(mail, Pattern))
        {
            return Result.Failure<Email>($"Email address '{mail}' is not valid.");
        }
        
        return new Email(mail);
    }
    
}