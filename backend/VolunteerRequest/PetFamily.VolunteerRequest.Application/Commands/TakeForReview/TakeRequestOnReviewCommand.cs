using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Commands.TakeForReview
{
    public record TakeRequestOnReviewCommand(Guid AdminId, Guid VolunteerRequestId) : ICommand;
    
}
