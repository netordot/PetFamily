using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Providers;
using PetFamily.Application.Species;
using PetFamily.Domain;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Pet.Species;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Requisites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.AddPet
{
    public class AddPetHandler : ICommandHandler<Guid, AddPetCommand>
    {
        private readonly IFileProvider _fileProvider;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public AddPetHandler(
            IFileProvider fileProvider,
            IVolunteerRepository volunteerRepository,
            ISpeciesRepository species,
            IReadDbContext context,
            IUnitOfWork unitOfWork)
        {
            _fileProvider = fileProvider;
            _volunteerRepository = volunteerRepository;
            _speciesRepository = species;
            _readDbContext = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ErrorList>> Handle(AddPetCommand command, CancellationToken cancellationToken)
        {
            var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error.ToErrorList();
            }

            if(await SpeciesBreesExists(command.SpeciesId, command.BreedId) == false)
            {
                return Errors.General.NotFound().ToErrorList();
            }

            var speciesBreed = new SpeciesBreed(SpeciesId.Create(command.SpeciesId), command.BreedId);

            var phoneNumberResult = volunteerResult.Value.Number;
            var addressResult = volunteerResult.Value.Address;

            // TODO: решить проблему с readonly списками, прийти к единому стандарту
            var requisites = (volunteerResult.Value.Requisites).ToList();

            var petId = PetId.NewPetId;

            var pet = Pet.Create(command.Name,
                speciesBreed,
                command.Color,
                command.Description,
                command.HealthCondition,
                phoneNumberResult,
                addressResult,
                requisites,
                command.status,
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

        private async Task<bool> SpeciesBreesExists(Guid speciesId, Guid breedId)
        {
            var breedExists = await _readDbContext
                .Breeds
                .FirstOrDefaultAsync(b => b.Id == breedId && b.SpeciesId == speciesId);
            // проверка на случай нарушения целостности данных(спишес удален, но брид почему-то не удалился)
            var speciesExists = await _readDbContext
                .Species
                .FirstOrDefaultAsync(s => s.Id == speciesId);

            if (speciesExists == null || breedExists == null)
            {
                return false;
            }

            return true;
        }
    }
}
