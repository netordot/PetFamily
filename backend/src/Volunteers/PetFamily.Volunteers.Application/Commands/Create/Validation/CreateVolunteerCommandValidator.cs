using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application.Commands.Create;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.Create.Validation;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.Description).MaximumLength(Constants.MAX_LONG_TEXT_SIZE);
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => c.BuildingNumber).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("BuildingNumber"));
        RuleFor(c => c.CorpsNumber).GreaterThanOrEqualTo(0).WithError(Errors.General.ValueIsInvalid("CorpsNumber"));
        RuleFor(c => c.Experience).GreaterThanOrEqualTo(0).WithError(Errors.General.ValueIsInvalid("Experience"));

        RuleFor(c => c.City).NotEmpty();
        RuleFor(c => c.Street).NotEmpty();

        RuleFor(c => c.FirstName).MaximumLength(Constants.MAX_SHORT_TEXT_SIZE);
        RuleFor(c => c.LastName).MaximumLength(Constants.MAX_SHORT_TEXT_SIZE);
        RuleFor(c => c.MiddleName).MaximumLength(Constants.MAX_SHORT_TEXT_SIZE);

        RuleForEach(c => c.SocialNetworks).MustBeValueObject(s => Social.Create(s.Name, s.Link));
        RuleForEach(c => c.Requisites).MustBeValueObject(r => Requisite.Create(r.Title, r.Description));
    }
}