using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Application.Commands.CloseDiscussion
{
    public record CloseDiscussionCommand(Guid Adminid, Guid DiscussionId) : ICommand;
    
}
