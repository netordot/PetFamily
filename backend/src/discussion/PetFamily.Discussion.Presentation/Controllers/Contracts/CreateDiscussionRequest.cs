using PetFamily.Discussion.Application.Commands.CreateDiscussion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Presentation.Controllers.Contracts
{
    public record CreateDiscussionRequest(Guid AdminId, Guid UserId, Guid RelationId)
    {
        public CreateDiscussionCommand ToCommand()
        {
            return new CreateDiscussionCommand(RelationId, AdminId, UserId);
        }
    }

}
