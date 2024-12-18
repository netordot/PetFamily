using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Application.Commands.EditMessage
{
    public record EditMessageCommand(Guid DiscussionId, Guid UserId, Guid MessageId, string Text) : ICommand;

}
