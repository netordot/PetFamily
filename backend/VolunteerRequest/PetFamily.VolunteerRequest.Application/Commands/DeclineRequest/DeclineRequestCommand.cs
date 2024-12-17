using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Commands.DeclineRequest
{
    public record DeclineRequestCommand (Guid RequestId, Guid AdminId, string RejectionDescription) : ICommand;
    
}
