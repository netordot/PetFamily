using FluentValidation;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public class UpdateRequisiteCommandValidator : AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisiteCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
    }
}