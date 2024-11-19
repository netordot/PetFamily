using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Providers
{
    public record FileData(Stream Stream, FileInfo FileInfo);
}
