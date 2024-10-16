using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Providers;
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

        public AddPetService(IFileProvider fileProvider, IVolunteerRepository volunteerRepository)
        {
            _fileProvider = fileProvider;
            _volunteerRepository = volunteerRepository;
        }

        public async Task<Result<Guid, Error>> AddPet(AddPetCommand command, CancellationToken cancellationToken)
        {
            var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            // достаем через агрегат характерные для пета поля

            var phoneNumberResult = volunteerResult.Value.Number;
            var addressResult = volunteerResult.Value.Address;


            var petId = PetId.NewPetId;
            //прописываем другие параметры

            // загрузка файлов в Minio, создание VO файлов, сохраненияе в бд

            // заглушка
            var fileList = command.Files.Select(f => PetPhoto.Create(
                f.fileName,
                false,
                PetPhotoId.NewPetPhotoId(Guid.NewGuid())).Value).ToList();

            //var petPhotos = new PetPhotos(fileList);

            var pet = Pet.Create(command.Name,
                //заглушка
                //command.SpeciesBreed.SpeciesBreed,
                null,
                command.Color,
                command.Description,
                command.HealthCondition,
                phoneNumberResult,
                addressResult,
                command.status,
                command.Height,
                command.Weight,
                command.IsCastrated,
                command.IsVaccinated,
                command.BirthDate,
                DateTime.Now,
                fileList,
                petId
                );

            if (pet.IsFailure)
                return pet.Error;

            volunteerResult.Value.AddPet(pet.Value);

            await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            return petId.Value;
            // вернуть айдишку пета
        }
    }
}
