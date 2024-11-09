using CSharpFunctionalExtensions;
using Microsoft.Extensions.FileProviders;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.FullDeletePet
{
    public class FullDeletePetHandler : ICommandHandler<Guid, FullDeletePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Providers.IFileProvider _fileProvider;
        private readonly string _bucket = "photos";

        public FullDeletePetHandler(IVolunteerRepository volunteerRepository, IUnitOfWork unitOfWork, Providers.IFileProvider fileProvider)
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
                var fileInfos = petFiles.Select(f => new Providers.FileProvider.FileInfo(f.Path, _bucket));

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
