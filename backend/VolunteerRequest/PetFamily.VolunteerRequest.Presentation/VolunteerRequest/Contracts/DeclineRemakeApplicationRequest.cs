using PetFamily.VolunteerRequest.Application.Commands.DeclineRequest;
using PetFamily.VolunteerRequest.Application.Commands.SetRequestForRemake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Presentation.VolunteerRequest.Contracts
{
    public record DeclineRemakeApplicationRequest(string RejectionComment)
    {
        public DeclineRequestCommand ToDeclineCommand(Guid adminId, Guid requestId)
        {
            return new DeclineRequestCommand(requestId, adminId, RejectionComment);
        }

        public SetRequestForRemakeCommand ToRemakeCommand(Guid adminId, Guid requestId)
        {
            return new SetRequestForRemakeCommand (adminId, requestId,  RejectionComment);
        }
    }
}
