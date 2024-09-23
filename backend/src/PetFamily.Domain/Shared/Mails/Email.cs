using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared;

public record Email
{
    private string Pattern { get; } = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\n";
    public string Mail { get;private set; }

    public Email(string mail)
    {
        if (Regex.IsMatch(mail, Pattern))
            Mail = mail;

        else
        {
            throw new ArgumentException($"Email address '{mail}' is not valid.");
        }
    }
    
}