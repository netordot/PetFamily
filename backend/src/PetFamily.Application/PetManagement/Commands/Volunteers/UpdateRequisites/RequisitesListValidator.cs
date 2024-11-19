using FluentValidation;
using PetFamily.Application.PetManagement.Commands.Volunteers.Create.Validation;
using PetFamily.Core.Validation;
using PetFamily.Domain;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateRequisites;

public class RequisitesListValidator : AbstractValidator<RequisiteListDto>
{
    public RequisitesListValidator()
    {
        RuleForEach(c => c.requisites).MustBeValueObject(r => Requisite.Create(r.Title, r.Description));
    }
}