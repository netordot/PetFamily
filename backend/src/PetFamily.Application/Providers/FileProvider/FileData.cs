using PetFamily.Domain.Pet.PetPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers.FileProvider
{
    public record FileData(Stream Content, FilePath FilePath, string BucketName);

    //public record FileContent(Stream Stream, string ObjectName);
}
