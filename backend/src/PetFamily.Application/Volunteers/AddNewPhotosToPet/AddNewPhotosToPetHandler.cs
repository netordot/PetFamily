using PetFamily.Application.Database;
using PetFamily.Application.Messaging;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.FileProviders;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Application.Providers;
using IFileProvider = PetFamily.Application.Providers.IFileProvider;

namespace PetFamily.Application.Volunteers.AddNewPhotosToPet
{
    public class AddNewPhotosToPetHandler : ICommandHandler<Guid, AddNewPetFilesCommand>
    {
        private readonly string _bucket = "photos";
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly Providers.IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageQueue<IEnumerable<Providers.FileProvider.FileInfo>> _messageQueue;

        public AddNewPhotosToPetHandler(IVolunteerRepository repository, 
            IFileProvider fileProvider, 
            IUnitOfWork unitOfWork, 
            IMessageQueue<IEnumerable<Providers.FileProvider.FileInfo>> messageQueue)
        {
            _volunteerRepository = repository;
            _messageQueue = messageQueue;
            _unitOfWork = unitOfWork;
            _fileProvider = fileProvider;
        }
      
        public async Task<CSharpFunctionalExtensions.Result<Guid, ErrorList>> Handle(AddNewPetFilesCommand command, CancellationToken cancellation)
        {
            var volunteer = await _volunteerRepository.GetById(command.VolunteerId, cancellation);

            var petToUpdate = volunteer.Value.GetPetById(command.PetId);
            if (petToUpdate.IsFailure)
            {
                return petToUpdate.Error.ToErrorList();
            }


            List<PetPhoto> photos = [];
            List<FileData> fileContents = [];

            foreach (var file in command.files)
            {
                var extension = Path.GetExtension(file.fileName);

                var filePath = FilePath.Create(Guid.NewGuid(), extension);
                if (filePath.IsFailure)
                    return filePath.Error.ToErrorList();

                var fileInfo = new Providers.FileProvider.FileInfo(filePath.Value, _bucket);

                var fileContent = new FileData(file.stream, fileInfo);
                fileContents.Add(fileContent);

            }

            var fileData = fileContents.ToList();

            var uploadResult = await _fileProvider.UploadFile(fileData, cancellation);
            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(fileData.Select(f => f.FileInfo), cancellation);

                return uploadResult.Error.ToErrorList();
            }

            var filePaths = command.files.Select(f => FilePath.Create(Guid.NewGuid(), f.fileName).Value);

            var petPhotos = filePaths.Select(p => PetPhoto.Create(p, false));

            photos = filePaths.Select(p => PetPhoto.Create(p, false).Value).ToList();

            var pictures = new ValueObjectList<PetPhoto>(photos);

            petToUpdate.Value.AddNewPhotos(pictures);
            var result =  _volunteerRepository.Save(volunteer.Value, cancellation);
            await _unitOfWork.SaveChanges(cancellation);

            return command.PetId;
        }
    }
}
