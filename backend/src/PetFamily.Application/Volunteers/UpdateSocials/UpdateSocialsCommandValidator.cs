using FluentValidation;
using PetFamily.Application.Volunteers.SharedDtos;
using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Application.Volunteers.UpdateSocials;

public class UpdateSocialsCommandValidator : AbstractValidator<UpdateSocialsCommand>
{
    public UpdateSocialsCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
    }
}