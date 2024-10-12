using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.FileProviders;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Photos.GetPhoto
{
    public class GetPhotoService
    {
        private readonly Providers.IFileProvider _fileProvider;

        public GetPhotoService(Providers.IFileProvider provider)
        {
            _fileProvider = provider;
        }

        public async Task<Result<string,Error>> Get(GetPhotoRequest request, CancellationToken ct)
        {
            if(string.IsNullOrWhiteSpace(request.FileName))
            {
                return Error.Validation("value.is.invalid", "file name");
            }
            var photo = new GetFileProvider(request.FileName);
            return await _fileProvider.GetFile(photo,ct);
        }
    }
}
