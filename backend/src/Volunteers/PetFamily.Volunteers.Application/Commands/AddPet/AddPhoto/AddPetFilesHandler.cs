using CSharpFunctionalExtensions;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Core.Messaging;
using PetFamily.Core.Providers;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.SharedKernel.Constraints;

namespace PetFamily.Volunteers.Application.Commands.AddPet.AddPhoto
{
    public class AddPetFilesHandler
    {
        private readonly string _bucket = "photos";
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly Core.Providers.IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageQueue<IEnumerable<Core.Providers.FileInfo>> _messageQueue;

        public AddPetFilesHandler(
            IVolunteerRepository repository,
            Core.Providers.IFileProvider fileProvider,
            [FromKeyedServices(ModuleNames.Volunteers)] IUnitOfWork unitOfWork,
            IMessageQueue<IEnumerable<Core.Providers.FileInfo>> messageQueue)
        {
            _volunteerRepository = repository;
            _fileProvider = fileProvider;
            _unitOfWork = unitOfWork;
            _messageQueue = messageQueue;
        }

        public async Task<Result<Guid, ErrorList>> Handle(Guid petId, Guid volunteerId, AddFileCommand command, CancellationToken cancellation)
        {

            var volunteer = await _volunteerRepository.GetById(volunteerId, cancellation);

            var petToUpdate = volunteer.Value.GetPetById(petId);
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

                var fileInfo = new Core.Providers.FileInfo(filePath.Value, _bucket);

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

            var filePaths = uploadResult.Value.Select(f => FilePath.Create(f.Path).Value);

            var petPhotos = filePaths.Select(p => PetPhoto.Create(p, false));

            photos = filePaths.Select(p => PetPhoto.Create(p, false).Value).ToList();

            //var pictures = new ValueObjectList<PetPhoto>(photos);

            petToUpdate.Value.UploadPhotos(photos);

            await _unitOfWork.SaveChanges(cancellation);

            //transaction.Commit();
            return petId;

        }
    }
}
