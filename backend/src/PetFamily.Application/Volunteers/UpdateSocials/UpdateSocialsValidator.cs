using FluentValidation;
using PetFamily.Application.Volunteers.SharedDtos;
using PetFamily.Application.Volunteers.Validation;
using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Application.Volunteers.UpdateSocials;

public class UpdateSocialsValidator : AbstractValidator<UpdateSocialsRequest>
{
    public UpdateSocialsValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
        // RuleForEach(c => c.socials).MustBeValueObject
        //     (s => Social.Create(s.Name, s.Link));
    }
}