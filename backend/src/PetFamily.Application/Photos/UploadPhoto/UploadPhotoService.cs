using CSharpFunctionalExtensions;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.FileProvider;
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

        //public async Task<Result<string, Error>> Upload(UploadPhotoRequest request, CancellationToken cancellation)
        //{
        //    // добавить валидацию
        //    var fileData = new FileData(request.Stream, request.BucketName, request.ObjectName);
        //    return await _fileProvider.UploadFile(fileData, cancellation);
        //}

    }
}
