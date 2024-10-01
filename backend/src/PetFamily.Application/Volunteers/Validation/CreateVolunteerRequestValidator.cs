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
        
        RuleFor(c=> c.City).NotEmpty();
        RuleFor(c=> c.Street).NotEmpty();
        
        RuleFor(c => c.FirstName).MaximumLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);
        RuleFor(c => c.LastName).MaximumLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);
        RuleFor(c => c.MiddleName).MaximumLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);
        
        RuleForEach(c => c.SocialNetworks).SetValidator(new SocialNetworkDtoValidator());
        RuleForEach(c => c.Requisites).SetValidator(new RequisiteDtoValidator());
        
    }
}