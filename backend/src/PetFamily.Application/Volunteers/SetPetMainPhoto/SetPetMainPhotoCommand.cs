using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.SetPetMainPhoto
{
    public record SetPetMainPhotoCommand(Guid VolunteerId, Guid PetId, string Path) : ICommand;
    
}
