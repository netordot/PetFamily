using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Querries.GetVolunteer
{
    public record GetVolunteerCommand(Guid id) : ICommand;
    
}
