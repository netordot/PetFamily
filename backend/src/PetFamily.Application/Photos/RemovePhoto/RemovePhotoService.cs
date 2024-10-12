using CSharpFunctionalExtensions;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Photos.RemovePhoto
{
    public class RemovePhotoService
    {
        private readonly IFileProvider _fileProvider;

        public RemovePhotoService(IFileProvider provider)
        {
            _fileProvider = provider;
        }

        public async Task<Result<string, Error>> Remove(string fileName, CancellationToken cancellationToken)
        {

            var result = await _fileProvider.RemoveFile(fileName, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result.Value;
        }
    }
}
