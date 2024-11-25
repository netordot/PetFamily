using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.AddPet.AddPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Commands.AddNewPhotosToPet
{
    public record AddNewPetFilesCommand(IEnumerable<FileDto> files, Guid PetId, Guid VolunteerId) : ICommand;

}
