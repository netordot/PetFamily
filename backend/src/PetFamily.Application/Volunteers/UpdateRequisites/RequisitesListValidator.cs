using FluentValidation;
using PetFamily.Application.Volunteers.Create.Validation;
using PetFamily.Domain;
using PetFamily.Domain.Shared.Requisites;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public class RequisitesListValidator : AbstractValidator<RequisiteListDto>
{
    public RequisitesListValidator()
    {
        RuleForEach(c => c.requisites).MustBeValueObject(r => Requisite.Create(r.Title, r.Description));
    }
}