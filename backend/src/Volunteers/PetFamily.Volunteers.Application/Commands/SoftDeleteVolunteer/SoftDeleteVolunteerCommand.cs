using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Commands.SoftDeleteVolunteer
{
    public record SoftDeleteVolunteerCommand(Guid Id) : ICommand;

}
