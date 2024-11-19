using PetFamily.Application.PetManagement.Commands.Volunteers.AddPet.AddPhoto;
using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.AddNewPhotosToPet
{
    public record AddNewPetFilesCommand(IEnumerable<FileDto> files, Guid PetId, Guid VolunteerId) : ICommand;

}
