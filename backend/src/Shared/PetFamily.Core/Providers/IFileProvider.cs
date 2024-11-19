using CSharpFunctionalExtensions;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Core.Providers;
using PetFamily.Domain.Shared.Errors;
using PetFamily.SharedKernel.ValueObjects;
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
        Task<Result<string, Error>> GetFile(Core.Providers.GetFileProvider provider, CancellationToken cancellation);

        Task<UnitResult<Error>> RemoveFile(Core.Providers.FileInfo fileInfo , CancellationToken cancellation);
    }
}
