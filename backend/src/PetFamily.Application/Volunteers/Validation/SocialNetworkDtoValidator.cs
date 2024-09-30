using FluentValidation;
using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Application.Volunteers.Validation;

public class SocialNetworkDtoValidator : AbstractValidator<SocialNetworkDto>
{
    public SocialNetworkDtoValidator()
    {
        RuleFor(s => new { s.Link, s.Name }).MustBeValueObject(x => Social.Create(x.Link, x.Name));
    }
}