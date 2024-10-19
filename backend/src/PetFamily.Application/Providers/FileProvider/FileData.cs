using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers.FileProvider
{
    public record FileData(IEnumerable<FileContent> Files, string BucketName);

    public record FileContent(Stream Stream, string ObjectName);
}
