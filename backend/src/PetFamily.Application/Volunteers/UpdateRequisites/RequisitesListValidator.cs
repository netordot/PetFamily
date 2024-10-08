using FluentValidation;
using PetFamily.Application.Volunteers.Validation;
using PetFamily.Domain;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public class RequisitesListValidator : AbstractValidator<RequisiteListDto>
{
    public RequisitesListValidator()
    {
        RuleForEach(c => c.requisites).MustBeValueObject(r => Requisite.Create(r.Title, r.Description));
    }
}