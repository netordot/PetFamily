using PetFamily.Discussion.Application.Commands.EditMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Presentation.Controllers.Contracts
{
    public record EditMessageRequest(string Text)
    {
        public EditMessageCommand ToCommand(Guid DiscussionId, Guid UserId, Guid MessageId)
        {
            return new EditMessageCommand(DiscussionId, UserId, MessageId,Text);
        }
    }
}
