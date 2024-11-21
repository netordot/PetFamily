using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Commands.FullDeletePet
{
    public record FullDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;

}
