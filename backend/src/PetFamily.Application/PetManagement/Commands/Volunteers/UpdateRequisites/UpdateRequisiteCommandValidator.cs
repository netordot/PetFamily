using FluentValidation;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateRequisites;

public class UpdateRequisiteCommandValidator : AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisiteCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
    }
}