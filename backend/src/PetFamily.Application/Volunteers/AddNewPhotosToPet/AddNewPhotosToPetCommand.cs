using PetFamily.Application.Abstractions;
using PetFamily.Application.Volunteers.AddPet.AddPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.AddNewPhotosToPet
{
    public record AddNewPetFilesCommand(IEnumerable<FileDto> files, Guid PetId, Guid VolunteerId) : ICommand;

}
