using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Application.Commands.DeleteMessage
{
    public record DeleteMessageCommand(Guid DiscussionId, Guid MessageId, Guid UserId) : ICommand;
    
}
