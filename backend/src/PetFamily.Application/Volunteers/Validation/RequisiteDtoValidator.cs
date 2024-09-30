using FluentValidation;
using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain;

namespace PetFamily.Application.Volunteers.Validation;

public class RequisiteDtoValidator : AbstractValidator<RequisiteDto>
{
    public RequisiteDtoValidator()
    {
        RuleFor(r => new { r.Description, r.Title }).MustBeValueObject(x => Requisite.Create(x.Title, x.Description));
    }
}