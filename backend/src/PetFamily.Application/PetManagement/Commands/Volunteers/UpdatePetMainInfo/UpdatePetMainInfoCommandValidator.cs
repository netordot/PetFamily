using FluentValidation;
using PetFamily.Application.PetManagement.Commands.Volunteers.Create.Validation;
using PetFamily.Core.Validation;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdatePetMainInfo
{
    public class UpdatePetMainInfoCommandValidator : AbstractValidator<UpdatePetMainInfoCommand>
    {
        public UpdatePetMainInfoCommandValidator()
        {
            RuleFor(c => c.Description).MaximumLength(Constants.MAX_LONG_TEXT_SIZE);
            RuleFor(c => c.BreedId).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.SpeciesId).NotEmpty();
            RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

            RuleFor(c => c.BuildingNumber).GreaterThan(0).WithError(Errors.General.ValueIsInvalid("BuildingNumber"));
            RuleFor(c => c.CorpsNumber).GreaterThanOrEqualTo(0).WithError(Errors.General.ValueIsInvalid("CorpsNumber"));

            RuleFor(c => c.City).NotEmpty();
            RuleFor(c => c.Street).NotEmpty();

            RuleForEach(c => c.Requisites).MustBeValueObject(r => Requisite.Create(r.Title, r.Description));
        }
    }
}
