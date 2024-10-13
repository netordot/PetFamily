using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers.FileProvider
{
    public record FileContent(Stream Stream, string BucketName, string ObjectName);
}
