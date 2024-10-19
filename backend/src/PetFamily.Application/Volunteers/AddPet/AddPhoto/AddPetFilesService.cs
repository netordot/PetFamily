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

namespace PetFamily.Application.Volunteers.AddPet.AddPhoto
{
    public class AddPetFilesService
    {
        // дальше допилить метод с созданием бакета в минио провайдере
        private readonly string _bucket = "photos";
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly Providers.IFileProvider _fileProvider;

        public AddPetFilesService(
            IVolunteerRepository repository, Providers.IFileProvider fileProvider)
        {
            _volunteerRepository = repository;
            _fileProvider = fileProvider;
        }

        public async Task<Result<Guid, Error>> AddPetFiles(AddFileCommand command, CancellationToken cancellation)
        {
            //логика нахождения пета по айдишнику

            List<PetPhoto> photos = [];
            List<FileContent> fileContents = [];

            foreach (var file in command.files)
            {
                var extension = Path.GetExtension(file.fileName);

                var filePath = FilePath.Create(Guid.NewGuid(), extension);
                if (filePath.IsFailure)
                    return filePath.Error;

                var fileContent = new FileContent(file.stream, filePath.Value.Path);
                fileContents.Add(fileContent);

                //var petPhoto = PetPhoto.Create(filePath.Value, false, PetPhotoId.NewPetPhotoId());
                //if (petPhoto.IsFailure)
                //    return petPhoto.Error;

                //photos.Add(petPhoto.Value);

            }

            var fileData = new FileData(fileContents, _bucket);

            var uploadResult = await _fileProvider.UploadFile(fileData, cancellation);
            if(uploadResult.IsFailure)
                return uploadResult.Error;

            var filePaths = command.files.Select(f => FilePath.Create(Guid.NewGuid(), f.fileName).Value);

            var petPhotos = filePaths.Select(p => PetPhoto.Create(p, false, PetPhotoId.NewPetPhotoId()));

            // добавили фото к пету
            // сохранили изменения

            // будем возвращать ид пета
            return Guid.NewGuid();

        }
    }
}
