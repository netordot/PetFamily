using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Commands.AddPet.AddPhoto
{
    public record AddFileCommand(IEnumerable<FileDto> files) : ICommand;

    public record FileDto(Stream stream, string contentType, string fileName);


}
