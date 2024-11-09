using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.ChangePetStatus
{
    public record ChangePetStatusCommand(PetStatusDto Status, Guid VolunteerId, Guid PetId) : ICommand;
    
}
