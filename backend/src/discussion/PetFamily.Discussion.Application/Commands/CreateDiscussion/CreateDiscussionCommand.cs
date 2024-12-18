using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.Discussion.Application.Commands.CreateDiscussion
{
    public record CreateDiscussionCommand(Guid RelationId, Guid AdminId, Guid UserId) : Core.Abstractions.ICommand;
    
}
