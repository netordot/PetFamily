using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.DeletePetPhoto
{
    public record DeletePetPhotoCommand(string Path, Guid PetId, Guid VolunteerId) : ICommand;

}
