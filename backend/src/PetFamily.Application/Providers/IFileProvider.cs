using CSharpFunctionalExtensions;
using PetFamily.Application.Providers.FileProvider;
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
        Task<UnitResult<Error>> UploadFile(FileData fileData, CancellationToken cancellation);
        //Task<Result<string, Error>> UploadFile(FileData content, CancellationToken cancellation);
        Task<Result<string, Error>> GetFile(GetFileProvider provider, CancellationToken cancellation);

        Task<Result<string, Error>> RemoveFile(string provider, CancellationToken cancellation);
    }
}
