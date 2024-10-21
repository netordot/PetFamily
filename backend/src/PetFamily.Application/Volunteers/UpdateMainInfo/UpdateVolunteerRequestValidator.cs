using FluentValidation;
using PetFamily.Application.Volunteers.Create.Validation;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public class UpdateVolunteerRequestValidator : AbstractValidator<UpdateVolunteerRequest>
{
    public UpdateVolunteerRequestValidator()
    {
        RuleFor(u => u.id).NotEmpty();
    }
}

public class UpdateVolunteerDtoValidator : AbstractValidator<UpdateVolunteerDto>
{
    public UpdateVolunteerDtoValidator()
    {
        RuleFor(c => c.Description).MaximumLength(Domain.Shared.Constants.MAX_LONG_TEXT_SIZE);
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => c.BuildingNumber).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("BuildingNumber"));
        RuleFor(c => c.CorpsNumber).GreaterThanOrEqualTo(0).WithError(Errors.General.ValueIsInvalid("CorpsNumber"));
        RuleFor(c => c.Experience).GreaterThanOrEqualTo(0).WithError(Errors.General.ValueIsInvalid("Experience"));

        RuleFor(c => c.City).NotEmpty();
        RuleFor(c => c.Street).NotEmpty();

        RuleFor(c => c.FirstName).MaximumLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);
        RuleFor(c => c.LastName).MaximumLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);
        RuleFor(c => c.MiddleName).MaximumLength(Domain.Shared.Constants.MAX_SHORT_TEXT_SIZE);
    }
    
}

