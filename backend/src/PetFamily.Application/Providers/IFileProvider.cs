using CSharpFunctionalExtensions;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers
{
    public interface IFileProvider 
    {
        Task<Result<IReadOnlyList<FilePath>, Error>> UploadFile(IEnumerable<FileData> filesData, CancellationToken cancellation);
        Task<Result<string, Error>> GetFile(GetFileProvider provider, CancellationToken cancellation);

        Task<Result<string, Error>> RemoveFile(string provider, CancellationToken cancellation);
    }
}
