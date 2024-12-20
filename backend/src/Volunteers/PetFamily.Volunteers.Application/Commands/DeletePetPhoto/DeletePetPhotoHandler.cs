﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Commands.DeletePetPhoto
{
    public class DeletePetPhotoHandler : ICommandHandler<Guid, DeletePetPhotoCommand>
    {
        private readonly string _bucket = "photos";
        private readonly IFileProvider _fileProvider;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePetPhotoHandler(IFileProvider fileProvider,
           [FromKeyedServices(ModuleNames.Volunteers)] IUnitOfWork unitOfWork,
            IVolunteerRepository volunteerRepository)
        {
            _fileProvider = fileProvider;
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ErrorList>> Handle(DeletePetPhotoCommand command, CancellationToken cancellation)
        {
            var volunteer = await _volunteerRepository.GetById(command.VolunteerId);
            if (volunteer.IsFailure)
            {
                return volunteer.Error.ToErrorList();
            }

            var pet = volunteer.Value.GetPetById(command.PetId);
            if (pet.IsFailure)
            {
                return pet.Error.ToErrorList();
            }

            var deleteFileInfo = new Core.Providers.FileInfo(FilePath.Create(command.Path).Value, _bucket);


            var result = await _fileProvider.RemoveFile(deleteFileInfo, cancellation);

            if (result.IsFailure)
            {
                return result.Error.ToErrorList();
            }

            var photoToRemove = pet.Value.Photos.FirstOrDefault(p => p.Path == FilePath.Create(command.Path).Value);
            if (photoToRemove == null)
            {
                return pet.Value.Id.Value;
            }

            pet.Value.DeletePhoto(photoToRemove);

            await _unitOfWork.SaveChanges(cancellation);

            return pet.Value.Id.Value;
        }
    }
}
