using FluentValidation;
using PetFamily.Application.PetManagement.Commands.Volunteers.Create.Validation;
using PetFamily.Application.PetManagement.Commands.Volunteers.SharedDtos;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateSocials;

public class SocialListDtoValidatior : AbstractValidator<SocialsListDto>
{
    public SocialListDtoValidatior()
    {
        RuleForEach(c => c.socials).MustBeValueObject(s => Social.Create(s.Name, s.Link));
    }
}