using CSharpFunctionalExtensions;
using Microsoft.Extensions.FileProviders;
using PetFamily.Application.PetManagement.Commands.Volunteers;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Volunteers.Application.Commands.FullDeletePet;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Domain.Entities;

namespace PetFamily.Application.Volunteers.FullDeletePet
{
    public class FullDeletePetHandler : ICommandHandler<Guid, FullDeletePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Core.Providers.IFileProvider _fileProvider;
        private readonly string _bucket = "photos";

        public FullDeletePetHandler(IVolunteerRepository volunteerRepository, IUnitOfWork unitOfWork, Core.Providers.IFileProvider fileProvider)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
            _fileProvider = fileProvider;
        }

        public async Task<Result<Guid, ErrorList>> Handle(FullDeletePetCommand command, CancellationToken cancellation)
        {
            var volunteer = await _volunteerRepository.GetById(command.VolunteerId);
            if (volunteer.IsFailure)
            {
                return volunteer.Error.ToErrorList();
            }

            var pet = volunteer.Value.GetPetById(command.PetId);
            if (pet.IsFailure)
            {
                return command.PetId;
            }

            if (pet.Value.Photos.Count() != 0)
            {
                var s3Result = await DeleteFilesFromS3(pet.Value, cancellation);
                if (s3Result.IsFailure)
                {
                    return s3Result.Error.ToErrorList();
                }
            }

            volunteer.Value.HardDeletePet(pet.Value);

            _volunteerRepository.Save(volunteer.Value);
            await _unitOfWork.SaveChanges(cancellation);

            return pet.Value.Id.Value;
        }

        private async Task<UnitResult<Error>> DeleteFilesFromS3(Pet pet, CancellationToken cancellation)
        {
            try
            {
                var petFiles = pet.Photos;
                var fileInfos = petFiles.Select(f => new Core.Providers.FileInfo(f.Path, _bucket));

                foreach (var fileInfo in fileInfos)
                {
                    await _fileProvider.RemoveFile(fileInfo, cancellation);
                }
                return Result.Success<Error>();
            }

            catch (Exception ex)
            {
                return Error.Failure("unable.delete", ex.Message);
            }
        }
    }
}
