using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Domain.Shared.PhoneNumber;

public record PhoneNumber
{
    private const string Pattern  = "^((\\+7|8)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$";
    public string Number { get;}

    private PhoneNumber(string number)
    {
        Number = number;
    }

    public static Result<PhoneNumber, Error> Create(string number)
    {
        if (String.IsNullOrWhiteSpace(number))
        {
            return Errors.Errors.General.ValueIsRequired("PhoneNumber");
        }
        if (!Regex.IsMatch(number, Pattern))
        {
            return Errors.Errors.General.ValueIsInvalid(number);
        }
       
        return new PhoneNumber(number);
    }
    
}