using PetFamily.Discussion.Application.Commands.CloseDiscussion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Presentation.Controllers.Contracts
{
    public record CloseDiscussionRequest(Guid AdminId)
    {
        public CloseDiscussionCommand ToCommand(Guid discussionId)
        {
            return new CloseDiscussionCommand(AdminId, discussionId);
        }
    }
}
