using PetFamily.VolunteerRequest.Application.Commands.TakeForReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Presentation.VolunteerRequest.Contracts
{
    public record TakeRequestOnReviewRequest(Guid AdminId)
    {
        public TakeRequestOnReviewCommand ToCommand(Guid requestId)
        {
            return new TakeRequestOnReviewCommand(AdminId, requestId);
        }
    }
}
