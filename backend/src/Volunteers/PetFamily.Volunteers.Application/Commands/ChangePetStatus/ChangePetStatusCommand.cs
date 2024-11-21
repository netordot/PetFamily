using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.PetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Commands.ChangePetStatus
{
    public record ChangePetStatusCommand(PetStatusDto Status, Guid VolunteerId, Guid PetId) : ICommand;

}
