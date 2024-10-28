using CSharpFunctionalExtensions;
using Microsoft.Extensions.FileProviders;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Pet;
using System.IO.Pipes;
using PetFamily.Domain.Shared;
using PetFamily.Application.Database;

namespace PetFamily.Application.Volunteers.AddPet.AddPhoto
{
    public class AddPetFilesService
    {
        private readonly string _bucket = "photos";
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly Providers.IFileProvider _fileProvider;
        private readonly IUnitOfWork _context;

        public AddPetFilesService(
            IVolunteerRepository repository, Providers.IFileProvider fileProvider, IUnitOfWork context)
        {
            _volunteerRepository = repository;
            _fileProvider = fileProvider;
            _context = context;
        }

        public async Task<Result<Guid, Error>> AddPetFiles(Guid petId,Guid volunteerId, AddFileCommand command, CancellationToken cancellation)
        {
            var transaction = await _context.BeginTransaction(cancellation);

            var volunteer = await _volunteerRepository.GetById(volunteerId, cancellation);

            var petToUpdate = volunteer.Value.GetPetById(petId);
            if(petToUpdate.IsFailure)
            {
                return petToUpdate.Error;
            }
            

            List<PetPhoto> photos = [];
            List<FileData> fileContents = [];

            foreach (var file in command.files)
            {
                var extension = Path.GetExtension(file.fileName);

                var filePath = FilePath.Create(Guid.NewGuid(), extension);
                if (filePath.IsFailure)
                    return filePath.Error;

                var fileContent = new FileData(file.stream, filePath.Value, _bucket);
                fileContents.Add(fileContent);

            }

            var fileData = fileContents.ToList();

            var uploadResult = await _fileProvider.UploadFile(fileData, cancellation);
            if (uploadResult.IsFailure)
                return uploadResult.Error;

            var filePaths = command.files.Select(f => FilePath.Create(Guid.NewGuid(), f.fileName).Value);

            var petPhotos = filePaths.Select(p => PetPhoto.Create(p, false, PetPhotoId.NewPetPhotoId()));

            photos = filePaths.Select(p => PetPhoto.Create(p, false, PetPhotoId.NewPetPhotoId()).Value).ToList();

            var pictures = new ValueObjectList<PetPhoto>(photos);

            petToUpdate.Value.UploadPhotos(pictures);

            await _context.SaveChanges(cancellation);

            transaction.Commit();
            return petId;

        }
    }
}
