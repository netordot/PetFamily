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
        Task<Result<string, Error>> UploadFile(FileContent content, CancellationToken cancellation);
    }
}
