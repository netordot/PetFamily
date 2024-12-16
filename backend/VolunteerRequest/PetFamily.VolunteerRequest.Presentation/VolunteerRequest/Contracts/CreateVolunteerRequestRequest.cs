using PetFamily.Core.Dtos.VolunteerRequest;
using PetFamily.VolunteerRequest.Application.Commands.CreateVolunteerRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Presentation.VolunteerRequest.Contracts
{
    public record CreateVolunteerRequestRequest(VolunteerInfoDto InfoDto)
    {
        public CreateVolunteerRequestCommand ToCommand(Guid userId)
        {
            return new CreateVolunteerRequestCommand(userId, InfoDto);
        }
    }
    
}
