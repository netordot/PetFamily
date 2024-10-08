using FluentValidation;
using PetFamily.Application.Volunteers.SharedDtos;
using PetFamily.Application.Volunteers.Validation;
using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Application.Volunteers.UpdateSocials;

public class SocialListDtoValidatior : AbstractValidator<SocialsListDto> 
{
    public SocialListDtoValidatior()
    {
        RuleForEach(c => c.socials).MustBeValueObject(s => Social.Create(s.Name, s.Link));
    }
}