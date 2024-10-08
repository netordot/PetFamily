using FluentValidation;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public class UpdateRequisiteRequestValidator : AbstractValidator<UpdateRequisitesRequest>
{
    public UpdateRequisiteRequestValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
    }
}