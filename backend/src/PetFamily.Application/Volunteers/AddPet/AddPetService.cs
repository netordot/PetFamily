using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Providers;
using PetFamily.Application.Species;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.AddPet
{
    public class AddPetService
    {
        private readonly IFileProvider _fileProvider;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ISpeciesRepository _speciesRepository;

        public AddPetService(IFileProvider fileProvider, IVolunteerRepository volunteerRepository, ISpeciesRepository species)
        {
            _fileProvider = fileProvider;
            _volunteerRepository = volunteerRepository;
            _speciesRepository = species;
        }

        public async Task<Result<Guid, Error>> AddPet(AddPetCommand command, CancellationToken cancellationToken)
        {
            var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var speciesBreed = await _speciesRepository
                .GetSpeciesBreedByNames(command.Species, command.Breed, cancellationToken);
            if(speciesBreed.IsFailure)
                return speciesBreed.Error;

            var phoneNumberResult = volunteerResult.Value.Number;
            var addressResult = volunteerResult.Value.Address;

            var requisitesResult = (volunteerResult.Value.Requisites);

            var petId = PetId.NewPetId;
 
            var pet = Pet.Create(command.Name,
                speciesBreed.Value,
                command.Color,
                command.Description,
                command.HealthCondition,
                phoneNumberResult,
                addressResult,
                requisitesResult,
                command.status,
                command.Height,
                command.Weight,
                command.IsCastrated,
                command.IsVaccinated,
                command.BirthDate,
                DateTime.UtcNow,
                null,
                //volunteerResult.Value.Id,
                petId
                );

            if (pet.IsFailure)
                return pet.Error;

            volunteerResult.Value.AddPet(pet.Value);
            int a = 10;

            await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            return petId.Value;
        }
    }
}
