using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.PetManagement.Commands.Volunteers;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Contracts;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Domain.Entities;
using PetFamily.Volunteers.Domain.Enums;
using PetFamily.Volunteers.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.AddPet
{
    public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
    {
        private readonly IFileProvider _fileProvider;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly ISpeciesContract _speciesContract;
        private readonly IUnitOfWork _unitOfWork;

        public AddPetHandler(
            IFileProvider fileProvider,
            IVolunteerRepository volunteerRepository,
            IReadDbContext context,
            ISpeciesContract speciesContract,
            IUnitOfWork unitOfWork)
        {
            _fileProvider = fileProvider;
            _volunteerRepository = volunteerRepository;
            _readDbContext = context;
            _speciesContract = speciesContract;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ErrorList>> Handle(AddPetCommand command, CancellationToken cancellationToken)
        {
            var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error.ToErrorList();
            }

            if (await _speciesContract.SpeciesBreedExists(command.SpeciesId, command.BreedId, cancellationToken) == false)
            {
                return Errors.General.NotFound().ToErrorList();
            }

            var speciesBreed = new SpeciesBreed(SpeciesId.Create(command.SpeciesId), command.BreedId);

            var phoneNumberResult = volunteerResult.Value.Number;
            var addressResult = volunteerResult.Value.Address;

            // TODO: решить проблему с readonly списками, прийти к единому стандарту
            var requisites = volunteerResult.Value.Requisites.ToList();

            var petId = PetId.NewPetId;
            var status = (PetStatus)(int)(command.status);

            var pet = Pet.Create(command.Name,
                speciesBreed,
                command.Color,
                command.Description,
                command.HealthCondition,
                phoneNumberResult,
                addressResult,
                requisites,
                status,
                command.Height,
                command.Weight,
                command.IsCastrated,
                command.IsVaccinated,
                command.BirthDate,
                DateTime.UtcNow,
                null,
                petId
                );

            if (pet.IsFailure)
                return pet.Error.ToErrorList();

            volunteerResult.Value.AddPet(pet.Value);
            int a = 10;

            await _unitOfWork.SaveChanges(cancellationToken);

            return petId.Value;
        }
    }
}
