using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Application.Queries.GetDiscussionWithMessages
{
    public record GetDiscussionWithMessagesQuery(Guid DiscussionId,Guid userId) : IQuery;
    
}
