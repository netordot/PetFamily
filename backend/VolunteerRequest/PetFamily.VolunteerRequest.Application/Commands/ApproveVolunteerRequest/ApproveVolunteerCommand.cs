using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.VolunteerRequest.Application.Commands.ApproveVolunteerRequest
{
    public record ApproveVolunteerCommand(Guid AdminId, Guid VolunteerRequestId) : Core.Abstractions.ICommand;
    {
    }
}
