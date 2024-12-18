using PetFamily.Discussion.Application.Commands.SendMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Presentation.Controllers.Contracts
{
    public record SendMessageRequest(string Message)
    {
        public SendMessageCommand ToCommand(Guid userId, Guid discussionId)
        {
            return new SendMessageCommand(userId, discussionId, Message);
        }
    }
}
