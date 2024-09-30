using FluentValidation;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;

namespace PetFamily.Application.Volunteers.Validation;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(c => c.Description).MaximumLength(Domain.Shared.Constants.MAX_LONG_TEXT_SIZE);
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        // накинуть еще 
        RuleForEach(c => c.SocialNetworks).SetValidator(new SocialNetworkDtoValidator());
        RuleForEach(c => c.Requisites).SetValidator(new RequisiteDtoValidator());
        
    }
}