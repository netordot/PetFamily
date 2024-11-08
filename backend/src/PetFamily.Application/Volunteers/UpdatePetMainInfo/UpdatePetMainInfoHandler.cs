using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.Species;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Shared.Requisites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PetFamily.Application.Volunteers.UpdatePetMainInfo
{
    public record UpdatePetMainInfoHandler : ICommandHandler<Guid, UpdatePetMainInfoCommand>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdatePetMainInfoCommand> _validator;

        public UpdatePetMainInfoHandler(IReadDbContext readDbContext, IVolunteerRepository volunteerRepository, IUnitOfWork unitOfWork,
            IValidator<UpdatePetMainInfoCommand> validator)
        {
            _readDbContext = readDbContext;
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;

        }
        public async Task<Result<Guid, ErrorList>> Handle(
            UpdatePetMainInfoCommand command, 
            CancellationToken cancellation)
        {
            var validationResult = await _validator.ValidateAsync(command,cancellation);
            if(validationResult.IsValid == false)
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

            if (await SpeciesBreedExists(command.SpeciesId, command.BreedId) == false)
            {
                return Errors.General.NotFound().ToErrorList();
            }

            var speciesBreed = new SpeciesBreed(SpeciesId.Create(command.SpeciesId), command.BreedId);
            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
            var requisites = new List<Requisite>();
            requisites = command.Requisites.Select(r => Requisite.Create(r.Title, r.Description).Value).ToList();

            var address = new Address(command.City, command.Street, command.BuildingNumber, command.CorpsNumber);

            pet.Value.UpdatePet(
                command.Name,
                speciesBreed,
                command.Color,
                command.Description,
                command.HealthCondition,
                phoneNumber,
                address,
                requisites,
                command.status,
                command.Height,
                command.Weight,
                command.IsCastrated,
                command.IsVaccinated,
                command.BirthDate);

            var result = _volunteerRepository.Save(volunteer.Value, cancellation);
            await _unitOfWork.SaveChanges(cancellation);
            
            return result;
        }

        private async Task<bool> SpeciesBreedExists(Guid speciesId, Guid breedId)
        {
            var breedExists = await _readDbContext.Breeds.FirstOrDefaultAsync(b => b.Id == breedId);
            var speciesExists = await _readDbContext.Species.FirstOrDefaultAsync(s => s.Id == speciesId);

            if (speciesExists == null || breedExists == null)
            {
                return false;
            }

            return true;
        }
    }
}
