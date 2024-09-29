using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared;

public record Email
{
    private const string Pattern  = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
    public string Mail { get; }

    private Email(string mail)
    {
        Mail = mail;
    }

    public static Result<Email> Create(string mail)
    {
        if (!Regex.IsMatch(mail, Pattern))
        {
            return ($"Email address '{mail}' is not valid.");
        }
        
        return new Email(mail);
    }
    
}