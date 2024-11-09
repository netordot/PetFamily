using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.SoftDeletePet
{
    public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
   
}
