using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.FileProviders;
using IFileProvider = PetFamily.Core.Providers.IFileProvider;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Core.Messaging;
using PetFamily.Core.Providers;
using PetFamily.Volunteers.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.SharedKernel.Constraints;

namespace PetFamily.Volunteers.Application.Commands.AddNewPhotosToPet
{
    public class AddNewPhotosToPetHandler : ICommandHandler<Guid, AddNewPetFilesCommand>
    {
        private readonly string _bucket = "photos";
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageQueue<IEnumerable<Core.Providers.FileInfo>> _messageQueue;

        public AddNewPhotosToPetHandler(IVolunteerRepository repository,
            IFileProvider fileProvider,
            IMessageQueue<IEnumerable<Core.Providers.FileInfo>> messageQueue,
            [FromKeyedServices(ModuleNames.Volunteers)] IUnitOfWork unitOfWork)
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

            // раньше было как в строке ниже, сейчас поменял на обычный List<PetPhoto>
            //var pictures = new ValueObjectList<PetPhoto>(photos);

            petToUpdate.Value.AddNewPhotos(photos);
            await _unitOfWork.SaveChanges(cancellation);

            return command.PetId;
        }
    }
}
