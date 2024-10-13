using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Photos.UploadPhoto
{
    public interface IUploadPhotoService
    {
        Task<Result<string, Error>> Upload(UploadPhotoRequest request, CancellationToken cancellation);
    }
}