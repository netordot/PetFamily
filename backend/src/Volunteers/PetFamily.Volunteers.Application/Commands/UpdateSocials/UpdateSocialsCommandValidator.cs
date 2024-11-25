using FluentValidation;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateSocials;

public class UpdateSocialsCommandValidator : AbstractValidator<UpdateSocialsCommand>
{
    public UpdateSocialsCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
    }
}