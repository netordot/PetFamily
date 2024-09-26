using System.Text.RegularExpressions;

namespace PetFamily.Domain.Shared.PhoneNumber;

public record PhoneNumber
{
    private const string Pattern  = "^((\\+7|8)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$\n";
    public string Number { get; private set; }

    private PhoneNumber(string number)
    {
        Number = number;
    }

    public static Result<PhoneNumber> Create(string number)
    {
        if (!Regex.IsMatch(number, Pattern))
        {
            return ($"Invalid phone number: {number}");
        }
       
        return new PhoneNumber(number);
    }
    
}