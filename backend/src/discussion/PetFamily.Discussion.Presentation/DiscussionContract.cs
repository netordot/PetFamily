using CSharpFunctionalExtensions;
using PetFamily.Discussion.Contracts;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Presentation
{
    public class DiscussionContract : IDiscussionContract
    {
        public Task<UnitResult<ErrorList>> AddMessage(Guid DiscussionId, Guid UserId, string Message)
        {
            throw new NotImplementedException();
        }

        public Task<UnitResult<ErrorList>> CloseDiscussionById(Guid DiscussionId, Guid AdminId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Guid, ErrorList>> CreateDiscussion(Guid RelationId, Guid UserId, Guid AdminId)
        {
            throw new NotImplementedException();
        }
    }
}
