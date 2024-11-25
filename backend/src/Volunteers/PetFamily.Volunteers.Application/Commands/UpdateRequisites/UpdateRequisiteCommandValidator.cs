using FluentValidation;

namespace PetFamily.Volunteers.Application.Commands.UpdateRequisites;

public class UpdateRequisiteCommandValidator : AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisiteCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
    }
}