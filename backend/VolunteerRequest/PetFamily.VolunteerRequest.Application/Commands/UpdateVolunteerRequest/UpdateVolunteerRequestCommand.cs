using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.VolunteerRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Commands.UpdateVolunteerRequest
{
    public record UpdateVolunteerRequestCommand(Guid UserId, Guid VolunteerRequestId, VolunteerInfoDto VolunteerInfoDto) : ICommand;
    
}
