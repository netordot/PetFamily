using CSharpFunctionalExtensions;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Providers
{
    public interface IFileProvider
    {
        Task<Result<IReadOnlyList<FilePath>, Error>> UploadFile(IEnumerable<FileData> filesData, CancellationToken cancellation);
        Task<Result<string, Error>> GetFile(GetFileProvider provider, CancellationToken cancellation);

        Task<UnitResult<Error>> RemoveFile(FileInfo fileInfo, CancellationToken cancellation);
    }
}
