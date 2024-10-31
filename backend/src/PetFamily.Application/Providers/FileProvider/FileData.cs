using PetFamily.Domain.Pet.PetPhoto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers.FileProvider
{
    public record FileData(Stream Stream, FileInfo FileInfo);

    public record FileInfo(FilePath filePath, string BucketName);
}
