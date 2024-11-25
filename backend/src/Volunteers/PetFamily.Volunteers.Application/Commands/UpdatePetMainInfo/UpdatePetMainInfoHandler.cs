using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Contracts;
using PetFamily.Volunteers.Domain.Enums;
using PetFamily.Volunteers.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetMainInfo
{
    public record UpdatePetMainInfoHandler : ICommandHandler<Guid, UpdatePetMainInfoCommand>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdatePetMainInfoCommand> _validator;
        private readonly ISpeciesContract _speciesContract;

        public UpdatePetMainInfoHandler(IReadDbContext readDbContext,
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            ISpeciesContract speciesContract,
            IValidator<UpdatePetMainInfoCommand> validator)
        {
            _readDbContext = readDbContext;
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _speciesContract = speciesContract;

        }
        public async Task<Result<Guid, ErrorList>> Handle(
            UpdatePetMainInfoCommand command,
            CancellationToken cancellation)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellation);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteer = await _volunteerRepository.GetById(command.VolunteerId, cancellation);
            if (volunteer.IsFailure)
            {
                return volunteer.Error.ToErrorList();
            }

            var pet = volunteer.Value.GetPetById(command.PetId);
            if (pet.IsFailure)
            {
                return pet.Error.ToErrorList();
            }

            if (await _speciesContract.SpeciesBreedExists(command.SpeciesId, command.BreedId, cancellation) == false)
            {
                return Errors.General.NotFound().ToErrorList();
            }

            var speciesBreed = new SpeciesBreed(SpeciesId.Create(command.SpeciesId), command.BreedId);
            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
            var requisites = new List<Requisite>();
            requisites = command.Requisites.Select(r => Requisite.Create(r.Title, r.Description).Value).ToList();

            var address = new Address(command.City, command.Street, command.BuildingNumber, command.CorpsNumber);
            var status = (PetStatus)(int)command.status;

            pet.Value.UpdatePet(
                command.Name,
                speciesBreed,
                command.Color,
                command.Description,
                command.HealthCondition,
                phoneNumber,
                address,
                requisites,
                status,
                command.Height,
                command.Weight,
                command.IsCastrated,
                command.IsVaccinated,
                command.BirthDate);

            var result = _volunteerRepository.Save(volunteer.Value, cancellation);
            await _unitOfWork.SaveChanges(cancellation);

            return result;
        }
    }
}
