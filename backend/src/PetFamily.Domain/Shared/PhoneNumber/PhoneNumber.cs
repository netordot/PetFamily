using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared;

public class PhoneNumber
{
    private string Pattern { get; } = "^((\\+7|8)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$\n";
    
    public string Number { get; private set; }

    public PhoneNumber(string number)
    {
        if (Regex.IsMatch(number, Pattern))
        {
            Number = number;
        }
        else
        {
            throw new FormatException("Invalid phone number");
        }
    }
    
    
}