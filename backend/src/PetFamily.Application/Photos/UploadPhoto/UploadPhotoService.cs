using CSharpFunctionalExtensions;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Photos.UploadPhoto
{
    public class UploadPhotoService 
    {
        private readonly IFileProvider _fileProvider;

        public UploadPhotoService(IFileProvider provider)
        {
            _fileProvider = provider;
        }

        public async Task<Result<IReadOnlyList<FilePath>, Error>> Upload(UploadPhotoRequest request, CancellationToken cancellation)
        {
            // добавить валидацию
            var fileInfo = new Providers.FileProvider.FileInfo(FilePath.Create(request.ObjectName).Value, request.BucketName);
            var fileData = new FileData(request.Stream, fileInfo);
            var list = new List<FileData>();
            list.Add(fileData);

            return await _fileProvider.UploadFile(list , cancellation);
        }

    }
}
