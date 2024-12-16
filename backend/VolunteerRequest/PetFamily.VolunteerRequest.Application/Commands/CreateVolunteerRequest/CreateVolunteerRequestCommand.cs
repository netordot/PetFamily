using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.VolunteerRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = PetFamily.Core.Abstractions.ICommand;

namespace PetFamily.VolunteerRequest.Application.Commands.CreateVolunteerRequest
{
    public record CreateVolunteerRequestCommand(
        Guid ParticipantId,
        VolunteerInfoDto VolunteerInfo
        ) : ICommand;

}
