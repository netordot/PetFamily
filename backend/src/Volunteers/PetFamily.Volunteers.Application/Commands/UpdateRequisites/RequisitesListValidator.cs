using FluentValidation;
using PetFamily.Application.PetManagement.Commands.Volunteers.Create.Validation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdateRequisites;

public class RequisitesListValidator : AbstractValidator<RequisiteListDto>
{
    public RequisitesListValidator()
    {
        RuleForEach(c => c.requisites).MustBeValueObject(r => Requisite.Create(r.Title, r.Description));
    }
}