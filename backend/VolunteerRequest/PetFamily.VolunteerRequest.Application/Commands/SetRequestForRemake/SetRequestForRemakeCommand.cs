using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.VolunteerRequest.Application.Commands.SetRequestForRemake
{
    public record SetRequestForRemakeCommand(Guid AdminId, Guid DiscussionId, string RejectionComment) : Core.Abstractions.ICommand;
    
}
